using Itis.DotnetExam.Api.Contracts.Requests.User;
using Itis.DotnetExam.Api.Contracts.Requests.User.GetUserData;
using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Contracts.Requests.User.SignIn;
using Itis.DotnetExam.Api.Core.Requests.User.GetRating;
using Itis.DotnetExam.Api.Core.Requests.User.GetUserData;
using Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Core.Requests.User.SignIn;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Itis.DotnetExam.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Пользователь"
/// </summary>
[Route("[controller]")]
public class UserController : BaseController
{
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<RegisterUserResponse> RegisterUser(
        [FromServices] IMediator mediator,
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new RegisterUserCommand
            {
                UserName = request.UserName,
                Password = request.Password,
            }, cancellationToken);

    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <returns></returns>
    [HttpPost("signIn")]
    public async Task<SignInResponse> SignIn(
        [FromServices] IMediator mediator,
        [FromBody] SignInRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new SignInQuery
            {
                UserName = request.UserName,
                Password = request.Password,
            }, cancellationToken);

    /// <summary>
    /// Получить информацию о рейтинге пользователя
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpGet("getUserData")]
    public async Task<GetUserDataResponse> GetUserDataAsync(
        [FromServices] IMediator mediator, 
        CancellationToken cancellationToken)
        => await mediator.Send(new GetUserDataQuery(CurrentUserId), cancellationToken);
    
    /// <summary>
    /// Получить информацию о рейтинге пользователей
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpGet("raiting")]
    public async Task<GetRatingResponse> GetRatingAsync(
        [FromServices] IMediator mediator, 
        CancellationToken cancellationToken)
        => await mediator.Send(new GetRatingQuery(), cancellationToken);
}