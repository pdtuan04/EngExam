using Domain.Common;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public sealed class FlashCard : BaseEntity<Guid>, ISoftDeletable
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid UserId { get; init; }
        private readonly List<Word> _words = [];
        public ICollection<Word> Words
        {
            get { return _words; }
        }

        public bool IsDeleted { get ; set; }

        public void AddWord(Word word)
        {
            if (_words.Count >= 50) throw new InvalidWordCountException();
            if(_words.Any(w => w.Id == word.Id)) throw new DuplicateWordException();
            _words.Add(word);
        }
        public void RemoveWord(Word word)
        {
            _words.Remove(word);
        }
    }
}
