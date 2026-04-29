using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public sealed class FlashCard 
    {
        public required Guid Id { get; init; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public required Guid UserId { get; init; }
        private readonly List<Word> _words = [];
        public ICollection<Word> Words { get; private set; } = [];

        public void AddWord(Word word)
        {
            if (_words.Count >= 100) throw new InvalidWordCountException();
            if(_words.Any(w => w.Id == word.Id)) throw new DuplicateWordException();
            _words.Add(word);
        }
        public void RemoveWord(Word word)
        {
            _words.Remove(word);
        }
    }
}
