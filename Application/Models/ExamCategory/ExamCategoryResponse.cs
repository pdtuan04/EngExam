using Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamCategory
{
    public sealed record ExamCategoryResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public ExamCategoryResponse(Guid Id, string Name, string? Description, string? ImageUrl)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.ImageUrl = string.IsNullOrEmpty(ImageUrl) ? null : ImageUrl.GetFileUrl();
        }
    }
}