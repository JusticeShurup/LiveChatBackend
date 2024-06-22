using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.UserFeatures.Query
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.Users.Where(user => true).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
            
            var usersDtos = new List<UserDto>();
            foreach (var user in users)
            {
                usersDtos.Add(UserDto.FromUser(user));
            }
            return Task.FromResult(usersDtos);
        }
    }
}
