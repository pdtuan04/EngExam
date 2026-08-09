using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class WordGuessingRoom
    {
        public required Guid Id { get; init; }
        public required string RoomCode { get; init; }
        public Guid? Player1UserId { get; set; }
        public Guid? Player2UserId { get; set; }
        public string? Player1ConnectionId { get; set; }
        public string? Player2ConnectionId { get; set; }
        public int Player1Score { get; set; } = 0;
        public int Player2Score { get; set; } = 0;
        public IList<Vocabulary> Words { get; private set; } = [];
        public required int CurrentWordIndex { get; set; } = 0;
        public WordGuessingStatus Status { get; set; } = WordGuessingStatus.Waiting;
        public long Version { get; set; } = 0;
        public void LoadWords(IList<Vocabulary> words)
        {
            if (words == null || !words.Any())
            {
                throw new VocabularyNotFoundException("Can't load empty vocabulary list!");
            }
            Words = words;
            CurrentWordIndex = 0;
        }
        public void UpdateRoomStatus(WordGuessingStatus status)
        {
            if (this.Player1ConnectionId != null && this.Player2ConnectionId != null)
            {
                if (Status != WordGuessingStatus.Completed)
                {
                    Status = status;
                }
            }
        }
        public Vocabulary? GetCurrentWord()
        {
            if (CurrentWordIndex >= Words.Count || Words == null)
                return null;
            return Words[CurrentWordIndex];
        }
        public void SetWords(IList<Vocabulary> words)
        {
            if (words == null || !words.Any())
                throw new VocabularyNotFoundException("Can't load empty vocabulary list!");
            Words = words;
        }
        public void MoveToNextWord()
        {
            CurrentWordIndex++;
        }
        public void UpdatePlayerScore(Guid playerId)
        {
            if (playerId == Player1UserId)
            {
                Player1Score ++;
            }
            else if (playerId == Player2UserId)
            {
                Player2Score ++;
            }
        }
    }
}
