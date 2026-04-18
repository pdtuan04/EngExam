

using Application.Abstractions.Repositories.Read;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class UserReadRepository : GenericReadRepository<Domain.Entity.User, User>, IUserReadRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserReadRepository(UserManager<User> userManager, IMapper mapper) : base(null,mapper)
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
