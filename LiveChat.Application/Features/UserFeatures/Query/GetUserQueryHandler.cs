using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using LiveChat.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LiveChat.Application.Features.UserFeatures.Query
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
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

            return await Task.FromResult(new UserDto() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName });
        }
    }
}
