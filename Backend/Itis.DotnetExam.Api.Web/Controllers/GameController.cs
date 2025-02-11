using Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.Contracts.Requests.Game.MakeMove;
using Itis.DotnetExam.Api.Core.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Itis.DotnetExam.Api.Web.Controllers;

/// <summary>
/// Контроллер игры
/// </summary>
public class GameController : BaseController
{
    /// <summary>
    /// Присоединиться к игре
    /// </summary>
    [HttpPost("join")]
    public async Task<JoinGameResponse> JoinGame(
        [FromServices] IMediator mediator,
        [FromBody] JoinGameRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new JoinGameCommand
        {
            GameId = request.GameId,
            UserId = CurrentUserId
        }, cancellationToken);

    /// <summary>
    /// Сделать ход
    /// </summary>
    [HttpPost("move")]
    public async Task MakeMove(
        [FromQuery] int index,
        [FromServices] IBus bus,
        CancellationToken cancellationToken)
        => await bus.Publish(new MakeMoveRequest
        {
            Index = index,
            UserId = CurrentUserId
        }, cancellationToken);
}