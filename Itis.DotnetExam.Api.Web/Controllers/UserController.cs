﻿using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Contracts.Requests.User.SignIn;
using Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Core.Requests.User.SignIn;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Itis.DotnetExam.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Пользователь"
/// </summary>
public class UserController : BaseController
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public UserController()
    {
    }

    /// <summary>
    /// Зарегестрировать пользователя
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
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                Email = request.Email,
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
                Email = request.Email,
                Password = request.Password,
            }, cancellationToken);
}