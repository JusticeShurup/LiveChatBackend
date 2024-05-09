using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public required UserDto User { get; set; }
    }
}
