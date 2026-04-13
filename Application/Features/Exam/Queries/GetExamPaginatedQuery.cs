using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Queries
{
    public sealed record GetExamPaginatedQuery(int PageNumber = 1, int PageSize = 10) : IQuery<PaginationResponse<ExamResponse>>;
}
