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

namespace LiveChat.Application.Features.UserFeatures.Query
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetImageQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
        }

        public Task<string> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            string? userId = _contextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            User? user = _unitOfWork.Users.Get(user => user.Id == Guid.Parse(userId));

            if (user == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return Task.FromResult(Convert.ToBase64String(user.AvatarImage));
        }
    }
}
