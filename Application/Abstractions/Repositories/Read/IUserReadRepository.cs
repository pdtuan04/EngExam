using Application.Models.User;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IUserReadRepository
    {
        Task<UserDetailResponse> GetUserById(Guid id);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);
        Task UpdateUserAvatarAsync(Guid userId, string avatarUrl, DateTime updatedAt);
    }
}
