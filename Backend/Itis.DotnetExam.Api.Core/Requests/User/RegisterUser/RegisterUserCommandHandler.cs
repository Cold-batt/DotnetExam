using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Entities;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.Core.Extensions;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;

/// <summary>
/// Обработчик запроса <see cref="RegisterUserCommand"/>
/// </summary>
public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public RegisterUserCommandHandler(
        IUserService userService)
    {
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var isUserExist = await _userService.FindUserByEmailAsync(request.Email);
        if (isUserExist != null)
            throw new ValidationException("Пользователь с данной почтой уже существует");
        
        var user = new Entities.User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        
        var result = await _userService.RegisterUserAsync(user, request.Password);

        if (result.Succeeded)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Role, request.Role.ToUpperFirstCharString())
            };

            await _userService.AddUserRoleAsync(user, request.Role);
            await _userService.AddClaimsAsync(user, claims);
        }
        else
        {
            throw new ApplicationExceptionBase($"Не удалось зарегестрировать пользователя: {string.Join('\n', result.Errors.Select(x => x.Description))}");
        }
        
        return new RegisterUserResponse(result);
    }
}