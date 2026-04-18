using Application.Abstractions.Messaging;
using Application.Models.Exam;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Queries
{
    public sealed record GetExamResultPaginatedByUserIdQuery(Guid UserId, int PageIndex, int PageSize) : IQuery<PaginationResponse<ExamResultResponse>>;
}
