using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.UserFeatures.Command
{
    public class UpdateUserProfileCommand : IRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarImage { get; set; }
    }
}
