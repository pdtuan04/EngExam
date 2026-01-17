using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Repositories
{
    public interface IExamResultRepository
    {
        Task AddAsync(ExamResult examResult);
    }
}
