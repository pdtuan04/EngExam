using Application.Common;
using Application.DTOs;
using Application.DTOs.Responses;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Exam
{
    public interface IGetPaginatedExam
    {
        public Task<PaginatedList<ExamSummaryDto>> GetPaginatedExamsAsync(PaginatedDTO paginated);
    }
}
