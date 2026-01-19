using Application.DTOs;
using Application.DTOs.Responses;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IGetPaginated<T> where T : class
    {
        public Task<PaginatedList<ExamSummaryDto>> GetPaginatedExamsAsync(PaginatedDTO paginated);
    }
}
