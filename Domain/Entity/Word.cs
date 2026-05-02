using Domain.Abstractions.Entity;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public sealed class Word : IEntity<Guid>
    {
        public required Guid Id { get; init; }

        private string _text = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsMemorized { get; set; } = false;
        public required string Text
        {
            get => _text;
            init
            {
                _text = value?.Trim()??string.Empty;
            }
        }
        public required Guid FlashCardId { get; init; }
        private string _meaning = string.Empty;
        public string Meaning 
        { 
            get =>_meaning;
            private set => _meaning = value?.Trim() ?? string.Empty;
        }


        public void UpdateMeaning(string newMeaning)
        {
            if (string.IsNullOrWhiteSpace(newMeaning))
                throw new ArgumentException("The meaning of the word cannot be left blank.");

            _meaning = newMeaning?.Trim() ?? string.Empty;
        }
    }
}
