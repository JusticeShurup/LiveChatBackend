using Microsoft.AspNetCore.Identity;

namespace LiveChat.AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
    }
}
