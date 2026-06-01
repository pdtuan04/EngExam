using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class Question:BaseEntity, ISoftDeletable
    {
        public required string Content { get; set; }
        public QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public required Guid TopicId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? QuestionGroupId { get; set; }
    }
}
