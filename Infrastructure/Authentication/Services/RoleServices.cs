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
        public RoleServices(RoleManager<IdentityRole<Guid>> roleManager) 
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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
    }
}
