using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public sealed class Word
    {
        public required Guid Id { get; init; }
        private string _text = string.Empty;
        public required string Text
        {
            get => _text;
            init
            {
                _text = value?.Trim()??string.Empty;
            }
        }
        private string _meaning = string.Empty;
        public string Meaning 
        { 
            get =>_meaning;
            private set => _meaning = value?.Trim() ?? string.Empty;
        }
        public void UpdateMeaning(string newMeaning)
        {
            if (string.IsNullOrWhiteSpace(newMeaning))
                throw new ArgumentException("Nghĩa của từ không được để trống.");

            _meaning = newMeaning?.Trim() ?? string.Empty;
        }
    }
}
