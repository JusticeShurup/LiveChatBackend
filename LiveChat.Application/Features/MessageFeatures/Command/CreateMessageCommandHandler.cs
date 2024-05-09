using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using LiveChat.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.MessageFeatures.Command
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task<MessageDto> IRequestHandler<CreateMessageCommand, MessageDto>.Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            User? user = _unitOfWork.Users.Get(user => user.Id == request.UserId);
            if (user == null)
            {
                throw new ArgumentException("Request UserId isn't correct");
            }
            var message = new Message { Text = request.Text, CreatedTime = DateTime.UtcNow, User = user };
            _unitOfWork.Messages.Add(message);
            user.Messages.Append(message);
            _unitOfWork.SaveChanges();

            return new MessageDto
            {
                Text = message.Text,
                Id = message.Id,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AvatarImage = user.AvatarImage,
                }
            };
        }
    }
}
