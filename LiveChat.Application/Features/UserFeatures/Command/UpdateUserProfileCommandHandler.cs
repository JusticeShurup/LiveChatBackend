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
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IHttpContextAccessor _contextAccessor;

        public UpdateUserProfileCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = httpContextAccessor;
        }

        public Task Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            Guid? userId = Guid.Parse(_contextAccessor.HttpContext.User.FindFirst("Id")!.Value);
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            User? user = _unitOfWork.Users.Get(user => user.Id == userId);
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            user.FirstName = (request.FirstName != null && request.FirstName.Length != 0) ? request.FirstName : user.FirstName; 
            user.LastName = (request.LastName != null && request.LastName.Length != 0) ? request.LastName : user.LastName;
            try
            {
                string base64 = request.AvatarImage!.Substring("data:image/jpeg;base64,".Length);
                user.AvatarImage = (request.AvatarImage != null && request.AvatarImage.Length != 0) ? Convert.FromBase64String(base64) : user.AvatarImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            _unitOfWork.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
