using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entity
{
    public class QuestionGroup : BaseEntity<Guid>, ISoftDeletable
    {
        public string? Title { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        private readonly List<Question> _questions = [];
        public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();
        public bool IsDeleted { get; set; }
        public void AddQuestion(Question question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            _questions.Add(question);
        }
    }
}