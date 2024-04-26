using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public User User { get; set; }

        public Message() { }

        public Message(string text, User user)
        {
            Text = text;
            CreatedTime = DateTime.UtcNow;
            User = user;

        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Text)) 
            { 
                throw new ArgumentNullException($"Invalid message text {Text}"); 
            }
        }


    }
}
