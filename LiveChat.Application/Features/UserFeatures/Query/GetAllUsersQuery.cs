using LiveChat.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Features.UserFeatures.Query
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
