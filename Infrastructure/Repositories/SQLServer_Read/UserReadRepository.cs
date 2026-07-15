

using Application.Abstractions.Repositories.Read;
using Application.Common.Exceptions;
using Application.Models.User;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;
        public UserReadRepository(ApplicationDbReadContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            if(await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email) == null)
                return false;
            return true;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (result == null) return false;
            return true;
        }

        public async Task<UserDetailResponse> GetUserById(Guid id)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserDetailResponse>(dbUser);
        }

        public async Task UpdateUserAvatarAsync(Guid userId, string avatarUrl, DateTime updatedAt)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (dbUser == null) throw new NotFoundException("User", userId);
            if(dbUser.UpdatedAt < updatedAt)
            {
                dbUser.ImageUrl = avatarUrl;
                dbUser.UpdatedAt = updatedAt;
                _dbContext.Users.Update(dbUser);
                await _dbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task<int> GetCreatedUserCountByMonthAsync(int year, int month, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.CountAsync(u => u.CreatedAt.Year == year && u.CreatedAt.Month == month, cancellationToken);
        }

        public async Task<int> GetCreatedUserCountByYearAsync(int year, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.CountAsync(u => u.CreatedAt.Year == year, cancellationToken);
        }

        public async Task UpsertAsync(UserReadModel user)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (dbUser == null)
            {
                dbUser = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    NormalizedEmail = user.Email.ToUpperInvariant(),
                    NormalizedUserName = user.UserName.ToUpperInvariant(),
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };
                _dbContext.Users.Add(dbUser);
            }
            else
            {
                if (dbUser.UpdatedAt < user.UpdatedAt)
                {
                    dbUser.UserName = user.UserName;
                    dbUser.NormalizedUserName = user.UserName.ToUpperInvariant();
                    dbUser.Email = user.Email;
                    dbUser.NormalizedEmail = user.Email.ToUpperInvariant();
                    dbUser.ImageUrl = user.ImageUrl;
                    dbUser.UpdatedAt = user.UpdatedAt;
                    _dbContext.Users.Update(dbUser);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
