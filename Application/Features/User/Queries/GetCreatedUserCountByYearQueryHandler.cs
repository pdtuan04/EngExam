using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public sealed class GetCreatedUserCountByYearQueryHandler : IQueryHandler<GetCreatedUserCountByYearQuery, int>
    {
        private readonly IUserReadRepository _userReadRepository;
        public GetCreatedUserCountByYearQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }
        public async Task<int> Handle(GetCreatedUserCountByYearQuery request, CancellationToken cancellationToken)
        {
            return await _userReadRepository.GetCreatedUserCountByYearAsync(request.Year, cancellationToken);
        }
    }
}
