using LiveChat.Application.Dtos;
using LiveChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Services.Interface
{
    public interface IUserService
    {
        User CreateUser(UserDto userDto);
    }
}
