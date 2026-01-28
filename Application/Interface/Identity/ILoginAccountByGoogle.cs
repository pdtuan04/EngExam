using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Identity
{
    public interface ILoginAccountByGoogle
    {
        Task<LoginResponse> LoginByGoogle(string idToken);
    }
}
