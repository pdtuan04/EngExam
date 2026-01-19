using Application.DTOs;
using Application.DTOs.Responses;
using Application.Interface;
using Application.Repositories;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class GetPaginatedExam : IGetPaginated<ExamSummaryDto>
    {
        private readonly IExamRepository _examRepository;
        public GetPaginatedExam(IExamRepository examRepository)
        {
            _examRepository = examRepository ?? throw new ArgumentNullException();
        }
        public async Task<PaginatedList<ExamSummaryDto>> GetPaginatedExamsAsync(PaginatedDTO paginated)
        {
            var result = await _examRepository.GetPaginatedExamAsync(paginated.search, paginated.sortBy, paginated.sortDir.ToString(), paginated.pageNumber, paginated.pageSize);
            //them 1 query de lay total count nua thi co on khong
            return await PaginatedList<ExamSummaryDto>
                .CreatePageAsync(result.Items
                .Select(ex => new ExamSummaryDto
                {
                    Id = ex.Id,
                    Title = ex.Title,
                    Description = ex.Description
                }).ToList(), result.PageNumber, result.PageSize,result.TotalCount);
        }
    }
}
