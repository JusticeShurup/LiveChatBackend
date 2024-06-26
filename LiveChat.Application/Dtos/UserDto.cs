﻿using LiveChat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[]? AvatarImage { get; set; }

        public static UserDto FromUser(User user)
        {
            return new UserDto() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, AvatarImage = user.AvatarImage };
        }
    }
}
