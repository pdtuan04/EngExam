using Application.Abstractions.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class UserRepository : GenericRepository<Domain.Entity.User, User, Guid>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<User> userManager, IMapper mapper) : base(null,mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            if(await _userManager.FindByEmailAsync(email) == null)
                return false;
            return true;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var result = await _userManager.FindByNameAsync(username);
            if (result == null) return false;
            return true;
        }

        public async Task<Domain.Entity.User> GetUserById(Guid id)
        {
            var dbUser = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<Domain.Entity.User>(dbUser);
        }
    }
}
