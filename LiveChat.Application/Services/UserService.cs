using LiveChat.Application.Dtos;
using LiveChat.Application.Repository;
using LiveChat.Application.Services.Interface;
using LiveChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
     
        public UserService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public User CreateUser(UserDto userDto)
        {
            User user = new(userDto.Id, userDto.FirstName, userDto.LastName); 
            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
            
            return user;
        }
    }
}
