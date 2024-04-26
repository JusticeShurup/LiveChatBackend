using System.ComponentModel.DataAnnotations;

namespace LiveChat.AuthService.Models
{
    public class RevokedToken
    {
        [Key]
        public required string Token { get; set; }
        public DateTime RevokedTime { get; set; }
    }
}
