using Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Itis.DotnetExam.Api.SignalR.Hubs;

[Authorize]
public class TicTacToeHub: Hub<ITicTacToeHub>
{
    public async Task Join(Guid gameId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
    
}