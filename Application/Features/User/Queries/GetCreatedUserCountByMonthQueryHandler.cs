using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public sealed class GetCreatedUserCountByMonthQueryHandler : IQueryHandler<GetCreatedUserCountByMonthQuery, int>
    {
        private readonly IUserReadRepository _userReadRepository;
        public GetCreatedUserCountByMonthQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }
        public async Task<int> Handle(GetCreatedUserCountByMonthQuery request, CancellationToken cancellationToken)
        {
            return await _userReadRepository.GetCreatedUserCountByMonthAsync(request.Year, request.Month, cancellationToken);
        }
    }
}
