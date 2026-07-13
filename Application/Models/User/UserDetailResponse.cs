using Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.User
{
    public sealed record UserDetailResponse
    {
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string? ImageUrl { get; init; }
        public int? Age { get; init; }
        public UserDetailResponse(Guid Id, string UserName, string Email, string? ImageUrl, int? Age)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Email = Email;
            this.ImageUrl = string.IsNullOrEmpty(ImageUrl) ? null : ImageUrl.GetFileUrl();
            this.Age = Age;
        }
    }
}
