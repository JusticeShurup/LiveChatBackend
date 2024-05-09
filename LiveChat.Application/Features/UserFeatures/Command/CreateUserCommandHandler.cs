using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using LiveChat.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.UserFeatures.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirst("Id")!.Value);

            User user = new(userId, request.FirstName, request.LastName, new List<Message>());
            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();

            return await Task.FromResult(new UserDto { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName });
        }
    }
}
