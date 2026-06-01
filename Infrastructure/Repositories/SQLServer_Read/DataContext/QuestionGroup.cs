using Domain.Common;
using System;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class QuestionGroup : BaseEntity, ISoftDeletable
    {
        public string? Title { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}