using LiveChat.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.MessageFeatures.Command
{
    public class CreateMessageCommand : IRequest<MessageDto>
    {
        public required Guid UserId { get; set; }
        public required string Text { get; set; }
    }
}
