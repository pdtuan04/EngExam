using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPracticeRepository : IGenericRepository<Practice>
    {
        Task<Practice> GetPracticeToTake(Guid id);
    }
}
