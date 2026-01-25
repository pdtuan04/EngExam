using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRoleServices
    {
        Task<string> GetRole(string roleName);
        Task<string> CreateRole(string roleName);
        Task<bool> AddUserToRole(User user, string roleName);
    }
}
