using Application.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<Repositories.SQLServer.DataContext.User> _userManager;
        public RoleServices(RoleManager<IdentityRole<Guid>> roleManager
                            ,UserManager<Repositories.SQLServer.DataContext.User> userManager) 
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
        }

        public async Task<string> CreateRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            return roleName;
        }

        public async Task<string> GetRole(string roleName)
        {
            await _roleManager.FindByNameAsync(roleName);
            return roleName;
        }
        public async Task<bool> AddUserToRole(Domain.Entity.User request, string roleName)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) ?? throw new Exception("User not found");// tu tu tinh :))
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }
    }
}
