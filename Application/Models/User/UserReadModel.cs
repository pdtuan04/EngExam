using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.User
{
    public sealed record UserReadModel(Guid Id, string UserName, string Email, int? Age, string? ImageUrl, DateTime CreatedAt, DateTime UpdatedAt);
}
