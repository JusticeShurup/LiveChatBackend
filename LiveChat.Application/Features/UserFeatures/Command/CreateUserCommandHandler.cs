using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using LiveChat.Application.Services.Interface;
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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _userService.CreateUser(new UserDto
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            });
        }

    }
}
