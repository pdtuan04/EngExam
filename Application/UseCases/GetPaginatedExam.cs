using Application.Common;
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

namespace Application.UseCases
{
    public class GetPaginatedExam : IGetPaginatedExam
    {
        private readonly IExamRepository _examRepository;
        public GetPaginatedExam(IExamRepository examRepository)
        {
            _examRepository = examRepository ?? throw new ArgumentNullException();
        }
        public async Task<PaginatedList<ExamSummaryDto>> GetPaginatedExamsAsync(PaginatedDTO paginated)
        {
            var exams = await _examRepository.GetPaginatedExamAsync(paginated.search, paginated.sortBy, paginated.sortDir.ToString(), paginated.pageNumber, paginated.pageSize);
            var result = exams.Items.Select(exam => new ExamSummaryDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Description = exam.Description
            });
            return new PaginatedList<ExamSummaryDto>(result, exams.PageNumber, exams.PageSize, exams.TotalCount);
        }
    }
}
