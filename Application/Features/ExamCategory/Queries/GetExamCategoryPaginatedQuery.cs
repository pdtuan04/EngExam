using Application.Abstractions.Messaging;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Queries
{
    public sealed record GetExamCategoryPaginatedQuery(int PageNumber = 1, int PageSize = 10) : IQuery<PaginationResponse<ExamCategoryResponse>>;
}
