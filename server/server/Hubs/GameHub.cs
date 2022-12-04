using GuessingGame.DTO.responses;
using GuessingGame.hubs.Clients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.hubs;

[Authorize]
public class GameHub: Hub<IGameClient>
{
    // This method is called when a client connects to the hub
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("executing OnConnectedAsync");
        // Get connection ID
        var connectionId = Context.ConnectionId;
        // Get room ID from claim 'roomID'
        var roomId = Context.User.Claims.FirstOrDefault(c => c.Type == "roomID")?.Value;
        Console.WriteLine("Retrieved from claims: " + roomId + " " + connectionId);
        // Add to SignalR's room group
        Groups.AddToGroupAsync(connectionId, roomId);
        return base.OnConnectedAsync();

    }
}