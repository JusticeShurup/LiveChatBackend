﻿namespace LiveChat.AuthService.Models.Dtos.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
