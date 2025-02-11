using Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;
using Itis.DotnetExam.Api.Core.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Core.Requests.Game.GetGames;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itis.DotnetExam.Api.Web.Controllers;

/// <summary>
/// Контроллер игры
/// </summary>
[Authorize]
public class GameController : BaseController
{
    // private readonly HttpContext _httpContext;
    //
    // public GameController(HttpContext httpContext)
    // {
    //     _httpContext = httpContext;
    // }
    
    /// <summary>
    /// Получить список игр
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns>Список сущностей</returns>
    [HttpGet]
    public async Task<GetGamesResponse> GetAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetGamesRequest request,
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
    [HttpPost]
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
}