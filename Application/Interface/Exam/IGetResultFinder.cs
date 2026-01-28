using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Exam
{
    public interface IGetResultFinder
    {
        Task<IEnumerable<ExamResult>> GetResultsByUserId(Guid id);
        Task<ExamResult> GetResultById(Guid id);
    }
}
