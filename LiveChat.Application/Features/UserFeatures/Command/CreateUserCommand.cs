using LiveChat.Application.Dtos;
using MediatR;

namespace LiveChat.Application.Features.UserFeatures.Command
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
