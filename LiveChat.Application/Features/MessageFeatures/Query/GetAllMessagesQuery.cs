using LiveChat.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.MessageFeatures.Query
{
    public class GetAllMessagesQuery : IRequest<List<MessageDto>>
    {
    }
}
