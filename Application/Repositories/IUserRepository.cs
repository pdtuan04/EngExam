using Application.DTOs.Requests.Account;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(Guid id);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);
    }
}
