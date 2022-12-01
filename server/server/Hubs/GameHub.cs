using GuessingGame.DTO.responses;
using GuessingGame.hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace GuessingGame.hubs;

public class GameHub: Hub<IGameClient>
{
}