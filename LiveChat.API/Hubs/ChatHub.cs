using LiveChat.Application.Features.UserFeatures.Query;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace LiveChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private ISender _sender;

        public ChatHub(ISender sender)
        {
            _sender = sender;
        }

        public Task SendToOthers(string message)
        {
            _sender.Send(new GetUserQuery());
            return Clients.All.SendAsync("MessageReceived", message);
        }
    }
}
