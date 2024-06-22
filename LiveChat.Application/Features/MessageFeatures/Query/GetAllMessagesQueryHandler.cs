using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.MessageFeatures.Query
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, List<MessageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllMessagesQueryHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<MessageDto>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = _unitOfWork.Messages.Where(message => true, "User");

            var messageDtos = new List<MessageDto>();
            foreach (var message in messages)
            {
                messageDtos.Add(MessageDto.FromMessage(message));
            }
            return Task.FromResult(messageDtos);
        }
    }
}
