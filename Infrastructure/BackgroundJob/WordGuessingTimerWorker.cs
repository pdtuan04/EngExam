using Application.Common.Caching;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Models.Vocabulary;
using Application.Models.WordGuessing;
using Domain.Entity;
using Domain.Enums;
using Infrastructure.Cache;
using Infrastructure.Realtime;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.BackgroundJob
{
    public class WordGuessingTimerWorker : BackgroundService
    {
        private readonly IHubContext<WordGuessingHub> _hub;
        private readonly IConnectionMultiplexer _redis;
        public WordGuessingTimerWorker(IHubContext<WordGuessingHub> hub, IConnectionMultiplexer redis)
        {
            _hub = hub;
            _redis = redis;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var data = _redis.GetDatabase();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    var softTime = await data.SortedSetRangeByScoreAsync(CacheKeys.WordGuessingTimers, stop: now);
                    foreach (var item in softTime)
                    {
                        string roomCode = item.ToString();
                        var transaction = data.CreateTransaction();

                        var dataCache = await data.HashGetAsync(CacheKeys.GuessingRoom(roomCode), "Data");
                        var versionCache = await data.HashGetAsync(CacheKeys.GuessingRoom(roomCode), "Version");
                        if (!dataCache.HasValue || !versionCache.HasValue)
                        {
                            await data.SortedSetRemoveAsync(CacheKeys.WordGuessingTimers, roomCode);
                            continue;
                        }
                        var guessingRoomData = JsonSerializer.Deserialize<WordGuessingRoomCacheModel>(dataCache);
                        if (guessingRoomData == null || guessingRoomData.Player1ConnectionId == null || guessingRoomData.Player2ConnectionId == null)
                        {
                            await data.SortedSetRemoveAsync(CacheKeys.WordGuessingTimers, roomCode);
                            continue;
                        }

                        var words = guessingRoomData.Words.Select(w => new Vocabulary
                        {
                            Id = w.Id,
                            Word = w.Word,
                            Phonetic = w.Phonetic,
                            Meaning = w.Meaning,
                            PronunciationAudioUrl = w.PronunciationAudioUrl,
                            PartOfSpeech = w.PartOfSpeech
                        }).ToList();

                        var guessingRoom = new WordGuessingRoom
                        {
                            Id = guessingRoomData.Id,
                            RoomCode = guessingRoomData.RoomCode,
                            Player1ConnectionId = guessingRoomData.Player1ConnectionId,
                            Player2ConnectionId = guessingRoomData.Player2ConnectionId,
                            Player1UserId = guessingRoomData.Player1UserId,
                            Player2UserId = guessingRoomData.Player2UserId,
                            Player1Name = guessingRoomData.Player1Name,
                            Player2Name = guessingRoomData.Player2Name,
                            Player1Score = guessingRoomData.Player1Score,
                            Player2Score = guessingRoomData.Player2Score,
                            CurrentWordIndex = guessingRoomData.CurrentWordIndex,
                            Status = guessingRoomData.Status,
                            Version = guessingRoomData.Version
                        };
                        guessingRoom.SetWords(words);

                        transaction.AddCondition(Condition.HashEqual(CacheKeys.GuessingRoom(roomCode), "Version", versionCache));
                        guessingRoom.MoveToNextWord();
                        var isGameOver = guessingRoom.GetCurrentWord() == null;
                        guessingRoom.Version++;
                        if (isGameOver)
                        {
                            guessingRoom.UpdateRoomStatus(WordGuessingStatus.Completed);
                        }

                        var dataToStore = JsonSerializer.Serialize(guessingRoom);
                        _ = transaction.HashSetAsync(CacheKeys.GuessingRoom(roomCode), new HashEntry[]
                        {
                            new("Version", guessingRoom.Version),
                            new("Data", dataToStore)
                        });

                        if (isGameOver)
                        {
                            _ = transaction.SortedSetRemoveAsync(CacheKeys.WordGuessingTimers, roomCode);
                            _ = transaction.KeyExpireAsync(CacheKeys.GuessingRoom(roomCode), TimeSpan.FromMinutes(10));
                        }
                        else
                        {
                            long nextExpireTime = DateTimeOffset.UtcNow.AddSeconds(10).ToUnixTimeSeconds();
                            _ = transaction.SortedSetAddAsync(CacheKeys.WordGuessingTimers, roomCode, nextExpireTime);
                            _ = transaction.KeyExpireAsync(CacheKeys.GuessingRoom(roomCode), TimeSpan.FromMinutes(30));
                        }

                        var result = await transaction.ExecuteAsync();
                        if (result)
                        {
                            if (!isGameOver)
                            {
                                var currentWord = guessingRoom.GetCurrentWord();
                                var hiddenWord = new HiddenVocabularyResponse(
                                    currentWord.Id,
                                    currentWord.Word.MaskWord(),
                                    currentWord.Phonetic,
                                    currentWord.Meaning,
                                    currentWord.PronunciationAudioUrl,
                                    currentWord.PartOfSpeech);
                                await _hub.Clients.Group(roomCode).SendAsync("Timeout", "Time's up! No one got it.", stoppingToken);
                                await _hub.Clients.Group(roomCode).SendAsync("ReceiveWord", hiddenWord, stoppingToken);
                            }
                            else
                            {
                                await _hub.Clients.Group(roomCode).SendAsync("GameStatus", "Game over! All words have been guessed.");
                                string winnerMsg = "It's a tie!";
                                if (guessingRoom.Player1Score > guessingRoom.Player2Score) winnerMsg = "Player 1 wins!";
                                else if (guessingRoom.Player2Score > guessingRoom.Player1Score) winnerMsg = "Player 2 wins!";
                                var summary = new WordGuessingSummaryResponse(
                                    guessingRoom.Player1Name, guessingRoom.Player2Name,
                                    guessingRoom.Player1Score, guessingRoom.Player2Score, winnerMsg);
                                await _hub.Clients.Group(roomCode).SendAsync("GameOver", summary, stoppingToken);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}