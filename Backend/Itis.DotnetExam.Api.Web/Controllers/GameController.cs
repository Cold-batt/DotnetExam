using Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;
using Itis.DotnetExam.Api.Core.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Core.Requests.Game.GetGames;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.Contracts.Requests.Game.MakeMove;
using Itis.DotnetExam.Api.Core.Requests.Game.JoinGame;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Itis.DotnetExam.Api.Web.Controllers;

/// <summary>
/// Контроллер игры
/// </summary>
[Authorize]
[Route("[controller]")]
public class GameController : BaseController
{
    /// <summary>
    /// Получить список игр
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Список сущностей</returns>
    [HttpGet("games")]
    public async Task<GetGamesResponse> GetAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetGamesRequest? request,
        CancellationToken cancellationToken) =>
        await mediator.Send(
            request == null
                ? new GetGamesQuery()
                : new GetGamesQuery
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    IsAscending = request.IsAscending,
                },
            cancellationToken);
    
    /// <summary>
    /// Создание новой игры
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной игры</returns>
    [HttpPost("create")]
    public async Task<CreateGameResponse> CreateAsync(
        [FromServices] IMediator mediator,
        [FromBody] CreateGameRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        return await mediator.Send(
            new CreateGameCommand(CurrentUserId)
            {
                MaxRate = request.MaxRate,
            },
            cancellationToken);
    }
    
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