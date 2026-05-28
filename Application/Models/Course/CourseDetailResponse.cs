using Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Course
{
    public sealed record CourseDetailResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Content { get; init; }
        public string? ImageUrl { get; init; }
        public Guid TopicId { get; init; }
        public CourseDetailResponse(Guid Id, string Name, string Description, string Content, string? ImageUrl, Guid TopicId)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Content = Content;
            this.ImageUrl = string.IsNullOrWhiteSpace(ImageUrl) ? null : ImageUrl.GetFileUrl();
            this.TopicId = TopicId;
        }
    };
}
