using Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;
using Itis.DotnetExam.Api.SignalR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Itis.DotnetExam.Api.SignalR.Hubs;

[Authorize]
public class GameHub: Hub<IGameHub>
{
    /// <summary>
    /// Присоединиться в группу с игрой
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    public async Task Join(Guid gameId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    
    /// <summary>
    /// Отправить сообщение в чат
    /// </summary>
    public async Task SendMessageToChat(SendMessageModel sendMessageModel)
        => await Clients
            .GroupExcept(sendMessageModel.GameId.ToString(), Context.ConnectionId)
            .ReceiveMessage(sendMessageModel);
}