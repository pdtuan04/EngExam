using Application.DTOs.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IChangePasswordManager
    {
        Task<bool> ChangeUserPassword(ChangePasswordDTO request);
    }
}
