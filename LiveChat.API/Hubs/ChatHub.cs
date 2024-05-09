using LiveChat.Application.Dtos;
using LiveChat.Application.Features.MessageFeatures.Command;
using LiveChat.Application.Features.UserFeatures.Command;
using LiveChat.Application.Features.UserFeatures.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace LiveChat.API.Hubs
{
    [EnableCors("AllowAll")]
    public class ChatHub : Hub
    {
        private ISender _sender;
        private readonly IHttpContextAccessor _contextAccessor;

        public ChatHub(ISender sender, IHttpContextAccessor httpContextAccessor)
        {
            _sender = sender;
            _contextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task SendToOthers(string text)
        {
            var result = await _sender.Send(new GetUserQuery(){  });
            MessageDto message  = await _sender.Send(new CreateMessageCommand() { UserId = result.Id, Text = text });
            await Clients.All.SendAsync("MessageReceived", message);
        }
    }
}
