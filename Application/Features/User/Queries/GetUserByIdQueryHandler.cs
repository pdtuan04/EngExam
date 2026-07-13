using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDetailResponse>
    {
        private readonly IUserReadRepository _userReadRepository;
        public GetUserByIdQueryHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }
        public async Task<UserDetailResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userReadRepository.GetUserById(request.UserId);
        }
    }
}
