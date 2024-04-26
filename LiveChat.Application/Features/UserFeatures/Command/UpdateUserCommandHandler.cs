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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            string? userId = _contextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));   
            }
            User? user = _unitOfWork.Users.Get(user => user.Id == Guid.Parse(userId));
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            _unitOfWork.SaveChanges();
        }
    }
}
