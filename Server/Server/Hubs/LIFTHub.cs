using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class LIFTHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
