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
using Microsoft.AspNetCore.SignalR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Realtime
{
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
            WordGuessingRoom guessingRoom;
            var waitingRoomCode = await _database.ListRightPopAsync(CacheKeys.WaitingRooms);
            if (waitingRoomCode.HasValue)
            {
                string roomCode = waitingRoomCode.ToString();
                guessingRoom = await _cacheService.GetAsync<WordGuessingRoom>(CacheKeys.GuessingRoom(roomCode));
                if(guessingRoom == null || guessingRoom.Player1ConnectionId == null)
                {
                    await base.OnConnectedAsync();
                    return;
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
                var currentWord = guessingRoom.GetCurrentWord();
                var hiddenWordResponse = new HiddenVocabularyResponse
                {
                    Id = currentWord.Id,
                    HiddenWord = StringHelper.HideWord(currentWord.Word),
                    Meaning = currentWord.Meaning,
                    Phonetic = currentWord.Phonetic,
                    PronunciationAudioUrl = currentWord.PronunciationAudioUrl,
                    PartOfSpeech = currentWord.PartOfSpeech
                };
            }
            else
            {
                // Create new waiting room
            }
            await base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task<HiddenVocabularyResponse> GetHiddenVocabularyAsync()
        {
            var query = new GetRandomHiddenWordQuery();
            return await _sender.Send(query);
        }
        public async Task SubmitAnswerAsync(string user, string answer)
        {
            var command = new SubmitAnswerCommand(user, answer);
            await _sender.Send(command);
        }
    }
}
