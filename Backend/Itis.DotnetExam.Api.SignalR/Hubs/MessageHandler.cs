using Itis.DotnetExam.Api.SignalR.Events;
using Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;
using Itis.DotnetExam.Api.SignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace Itis.DotnetExam.Api.SignalR.Hubs;

public class MessageHandler(IHubContext<GameHub, IGameHub> hubContext) : IMessageHandler
{
    public async Task GameStarted(GameEvents.Start @event)
        => await hubContext.Clients.Group(@event.GameId.ToString()).GameStarted(@event);
    
    public async Task MoveMade(GameEvents.Move @event)
        => await hubContext.Clients.Group(@event.GameId.ToString()).MoveMade(@event);

    public async Task GameFinish(GameEvents.Finish @event)
        => await hubContext.Clients.Group(@event.GameId.ToString()).GameFinish(@event);

    public async Task ReceiveMessage(SendMessageModel message)
        => await hubContext.Clients.Group(message.GameId.ToString()).ReceiveMessage(message);
}