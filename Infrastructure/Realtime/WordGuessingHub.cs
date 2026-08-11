using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Common.Helpers;
using Application.Features.Vocabulary.Queries;
using Application.Models.Vocabulary;
using Application.Models.WordGuessing;
using Domain.Entity;
using Domain.Enums;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using OpenAI.Assistants;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Realtime
{
    [Authorize]
    public sealed class WordGuessingHub : Hub
    {
        private readonly ICacheService _cacheService;
        private readonly ISender _sender;
        private readonly IDatabase _database;
        public WordGuessingHub(ISender sender, ICacheService cacheService, IDatabase database)
        {
            _cacheService = cacheService;
            _sender = sender;
            _database = database;
        }
        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var userId = ClaimsExtensions.GetUserId(Context.User);
            var userName = ClaimsExtensions.GetUserName(Context.User);
            WordGuessingRoom guessingRoom = null;
            bool hasJoinedRoom = false;
            while (true)
            {
                var waitingRoomCode = await _database.ListRightPopAsync(CacheKeys.WaitingRooms);
                if (!waitingRoomCode.HasValue)
                    break;
                string roomCode = waitingRoomCode.ToString();
                var data = await _database.HashGetAsync(CacheKeys.GuessingRoom(roomCode), "Data");
                if (!data.HasValue) continue;
                guessingRoom = JsonSerializer.Deserialize<WordGuessingRoom>(data);
                if (guessingRoom == null || guessingRoom.Player1ConnectionId == null)
                {
                    continue;
                }
                guessingRoom.Player2ConnectionId = connectionId;
                guessingRoom.Player2UserId = userId;
                guessingRoom.Player2Name = userName;
                guessingRoom.UpdateRoomStatus(WordGuessingStatus.InProgress);
                var query = new GetRandomWordsQuery(10);
                var words = await _sender.Send(query);
                guessingRoom.LoadWords(words.Select(w => new Vocabulary
                {
                    Id = w.Id,
                    Word = w.Word,
                    Phonetic = w.Phonetic,
                    Meaning = w.Meaning,
                    PronunciationAudioUrl = w.PronunciationAudioUrl,
                    PartOfSpeech = w.PartOfSpeech
                }).ToList());
                //set the room code for player 2
                await _cacheService.SetAsync(CacheKeys.GuessingRoomByPlayer(connectionId), roomCode, TimeSpan.FromMinutes(30));
                await _database.HashSetAsync(CacheKeys.GuessingRoom(roomCode), new HashEntry[]
                {
                    new("Version", 1),
                    new("Data", JsonSerializer.Serialize(guessingRoom))
                });
                hasJoinedRoom = true;
                break;
            }
            if (!hasJoinedRoom)
            {
                var newRoomCode = CodeGenerator.GenerateRandomCode(6);
                guessingRoom = new WordGuessingRoom
                {
                    Id = Guid.CreateVersion7(),
                    Player1ConnectionId = connectionId,
                    Player1UserId = userId,
                    Player1Name = userName,
                    RoomCode = newRoomCode,
                    CurrentWordIndex = 0,
                    Status = WordGuessingStatus.Waiting,
                    Player1Score = 0,
                    Player2Score = 0,
                };
                await _database.ListLeftPushAsync(CacheKeys.WaitingRooms, newRoomCode);
                await _database.HashSetAsync(CacheKeys.GuessingRoom(newRoomCode), new HashEntry[]
                {
                    new("Version", 1),
                    new("Data", JsonSerializer.Serialize(guessingRoom))
                });
                await _cacheService.SetAsync(CacheKeys.GuessingRoomByPlayer(connectionId), newRoomCode, TimeSpan.FromMinutes(30));
            }
            await Groups.AddToGroupAsync(connectionId, guessingRoom.RoomCode);
            if(guessingRoom.Player1ConnectionId!=null && guessingRoom.Player2ConnectionId!=null)
            {
                await Clients.Group(guessingRoom.RoomCode).SendAsync("GameStatus", "Game started!");
                guessingRoom.UpdateRoomStatus(WordGuessingStatus.InProgress);
                var currentWord = guessingRoom.GetCurrentWord();
                await Clients.Group(guessingRoom.RoomCode).SendAsync("ReceiveWord", new HiddenVocabularyResponse(
                    currentWord.Id,
                    currentWord.Word.MaskWord(),
                    currentWord.Phonetic,
                    currentWord.Meaning,
                    currentWord.PronunciationAudioUrl,
                    currentWord.PartOfSpeech));
                await _database.HashSetAsync(CacheKeys.GuessingRoom(guessingRoom.RoomCode), new HashEntry[]
                {
                    new("Version", 1),
                    new("Data", JsonSerializer.Serialize(guessingRoom))
                });
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var guessingRoomCode = await _cacheService.GetAsync<string>(CacheKeys.GuessingRoomByPlayer(connectionId));
            if (!string.IsNullOrEmpty(guessingRoomCode))
            {
                await Clients.Group(guessingRoomCode).SendAsync("GameStatus", "A player has disconnected. The game will end.");
                await _cacheService.RemoveCacheAsync(CacheKeys.GuessingRoomByPlayer(connectionId));
                await _database.KeyDeleteAsync(CacheKeys.GuessingRoom(guessingRoomCode));
            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SubmitAnswerAsync(string answer)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                var playerId = ClaimsExtensions.GetUserId(Context.User);
                var userName = Context.User?.Identity?.Name ?? "Unknown";
                var roomCode = await _cacheService.GetAsync<string>(CacheKeys.GuessingRoomByPlayer(connectionId));
                if (string.IsNullOrEmpty(roomCode))
                {
                    await Clients.Caller.SendAsync("ErrorMessage", "You are not in a valid game room.");
                    return;
                }
                var data = await _database.HashGetAsync(CacheKeys.GuessingRoom(roomCode), "Data");
                var version = await _database.HashGetAsync(CacheKeys.GuessingRoom(roomCode), "Version");
                if (!data.HasValue || !version.HasValue) return;
                var guessingRoomData = JsonSerializer.Deserialize<WordGuessingRoomCacheModel>(data);

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
                if (guessingRoom == null || guessingRoom.Status != WordGuessingStatus.InProgress)
                {
                    await Clients.Caller.SendAsync("ErrorMessage", "The game room isn't available.");
                    return;
                }
                var currentWord = guessingRoom.GetCurrentWord();
                var isCorrect = string.Equals(currentWord.Word, answer, StringComparison.OrdinalIgnoreCase);
                if (isCorrect)
                {

                    guessingRoom.UpdatePlayerScore(playerId);
                    guessingRoom.MoveToNextWord();
                    bool isGameOver = guessingRoom.GetCurrentWord() == null;
                    var dataToStore = JsonSerializer.Serialize(guessingRoom);
                    var transaction = _database.CreateTransaction();
                    transaction.AddCondition(Condition.HashEqual(CacheKeys.GuessingRoom(roomCode), "Version", version));
                    guessingRoom.Version++;
                    _ = transaction.HashSetAsync(CacheKeys.GuessingRoom(roomCode), new HashEntry[]
                    {
                    new("Version", guessingRoom.Version),
                    new("Data", dataToStore)
                    });
                    var ttl = isGameOver ? TimeSpan.FromMinutes(10) : TimeSpan.FromMinutes(30);
                    _ =  transaction.KeyExpireAsync(CacheKeys.GuessingRoom(roomCode), ttl);
                    var result = await transaction.ExecuteAsync();
                    if (!result)
                    {
                        await Clients.Caller.SendAsync("ErrorMessage", "Your opponent has made a faster correct guess.");
                        return;
                    }
                    await Clients.Group(guessingRoom.RoomCode).SendAsync("CorrectAnswer", new WordGuessingAnswerResponse(true, userName, answer, guessingRoom.Player1Score, guessingRoom.Player2Score));
                    if (guessingRoom.GetCurrentWord() != null)
                    {
                        await Clients.Group(guessingRoom.RoomCode).SendAsync("GameStatus", "Ready for the next word! Good luck!");
                        var nextWord = guessingRoom.GetCurrentWord();
                        await Clients.Group(guessingRoom.RoomCode).SendAsync("ReceiveWord", new HiddenVocabularyResponse(
                        nextWord.Id,
                        nextWord.Word.MaskWord(),
                        nextWord.Phonetic,
                        nextWord.Meaning,
                        nextWord.PronunciationAudioUrl,
                        nextWord.PartOfSpeech));
                    }
                    else
                    {
                        guessingRoom.UpdateRoomStatus(WordGuessingStatus.Completed);
                        await Clients.Group(guessingRoom.RoomCode).SendAsync("GameStatus", "Game over! All words have been guessed.");
                        if (guessingRoom.Player1Score > guessingRoom.Player2Score)
                        {
                            await Clients.Group(guessingRoom.RoomCode).SendAsync("GameOver", new WordGuessingSummaryResponse(guessingRoom.Player1Name, guessingRoom.Player2Name, guessingRoom.Player1Score, guessingRoom.Player2Score, "Player 1 wins!"));
                        }
                        else if (guessingRoom.Player2Score > guessingRoom.Player1Score)
                        {
                            await Clients.Group(guessingRoom.RoomCode).SendAsync("GameOver", new WordGuessingSummaryResponse(guessingRoom.Player1Name, guessingRoom.Player2Name, guessingRoom.Player1Score, guessingRoom.Player2Score, "Player 2 wins!"));
                        }
                        else
                        {
                            await Clients.Group(guessingRoom.RoomCode).SendAsync("GameOver", new WordGuessingSummaryResponse(guessingRoom.Player1Name, guessingRoom.Player2Name, guessingRoom.Player1Score, guessingRoom.Player2Score, "It's a tie!"));
                        }
                    }
                }
                else
                {
                    await Clients.Group(guessingRoom.RoomCode).SendAsync("IncorrectAnswer", new { UserName = userName, Answer = answer });
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ErrorMessage", $"An error occurred: {ex.Message}");
            }
        }
    }
}
