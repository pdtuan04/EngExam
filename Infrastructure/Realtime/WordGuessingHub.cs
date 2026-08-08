using Application.Abstractions.Caching;
using Application.Abstractions.Repositories.Read;
using Application.Common.Caching;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Features.Vocabulary.Queries;
using Application.Models.Vocabulary;
using Domain.Entity;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            WordGuessingRoom guessingRoom = null;
            bool hasJoinedRoom = false;
            while (true)
            {
                var waitingRoomCode = await _database.ListRightPopAsync(CacheKeys.WaitingRooms);
                if (!waitingRoomCode.HasValue)
                    break;
                string roomCode = waitingRoomCode.ToString();
                guessingRoom = await _cacheService.GetAsync<WordGuessingRoom>(CacheKeys.GuessingRoom(roomCode));
                if (guessingRoom == null || guessingRoom.Player1ConnectionId == null)
                {
                    continue;
                }
                guessingRoom.Player2ConnectionId = connectionId;
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
                await _cacheService.SetAsync(CacheKeys.GuessingRoom(roomCode), guessingRoom, TimeSpan.FromMinutes(10));
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
                    RoomCode = newRoomCode,
                    CurrentWordIndex = 0,
                    Status = WordGuessingStatus.Waiting,
                    Player1Score = 0,
                    Player2Score = 0,
                };
                await _database.ListLeftPushAsync(CacheKeys.WaitingRooms, newRoomCode);
                await _cacheService.SetAsync(CacheKeys.GuessingRoom(newRoomCode), guessingRoom, TimeSpan.FromMinutes(30));
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
                await _cacheService.SetAsync(CacheKeys.GuessingRoom(guessingRoom.RoomCode), guessingRoom, TimeSpan.FromMinutes(10));
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
            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SubmitAnswerAsync(string user, string answer)
        {
            var connectionId = Context.ConnectionId;
            var roomCode = await _cacheService.GetAsync<string>(CacheKeys.GuessingRoomByPlayer(connectionId));
            if(string.IsNullOrEmpty(roomCode))
            {
                await Clients.Caller.SendAsync("ErrorMessage", "You are not in a valid game room.");
                return;
            }
            var guessingRoom = await _cacheService.GetAsync<WordGuessingRoom>(CacheKeys.GuessingRoom(roomCode));
            if (guessingRoom == null || guessingRoom.Status != WordGuessingStatus.InProgress)
            {
                await Clients.Caller.SendAsync("ErrorMessage", "The game room isn't available.");
                return;
            }
            var currentWord = guessingRoom.GetCurrentWord();
            var isCorrect = string.Equals(currentWord.Word, answer, StringComparison.OrdinalIgnoreCase);
            if (isCorrect)
            {
                await Clients.Group(guessingRoom.RoomCode).SendAsync("CorrectAnswer", new { User = user, Answer = answer , Mes = $"{user} has submitted a correct answer!"});
                guessingRoom.UpdatePlayerScore(connectionId);
                guessingRoom.MoveToNextWord();
                
                await _cacheService.SetAsync(CacheKeys.GuessingRoom(guessingRoom.RoomCode), guessingRoom, guessingRoom.Status == WordGuessingStatus.InProgress ? TimeSpan.FromMinutes(10) : TimeSpan.FromMinutes(30));
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
                    if(guessingRoom.Player1Score > guessingRoom.Player2Score)
                    {
                        await Clients.Group(guessingRoom.RoomCode).SendAsync("GameOver", "Player 1 wins!", guessingRoom.Player1Score, guessingRoom.Player2Score);
                    }
                    else if(guessingRoom.Player2Score > guessingRoom.Player1Score)
                    {
                        await Clients.Group(guessingRoom.RoomCode).SendAsync("GameOver", "Player 2 wins!", guessingRoom.Player1Score, guessingRoom.Player2Score);
                    }
                    else
                    {
                        await Clients.Group(guessingRoom.RoomCode).SendAsync("GameOver", "It's a tie!", guessingRoom.Player1Score, guessingRoom.Player2Score);
                    }   
                }
            }
            else
            {
                await Clients.Group(guessingRoom.RoomCode).SendAsync("IncorrectAnswer", new { User = user, Answer = answer });
            }
        }
    }
}
