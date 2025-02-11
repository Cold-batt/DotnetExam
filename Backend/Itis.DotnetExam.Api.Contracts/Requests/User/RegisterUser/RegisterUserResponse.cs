using Microsoft.AspNetCore.Identity;

namespace Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;

/// <summary>
/// Ответ на запрос <see cref="RegisterUserRequest"/>
/// </summary>
public class RegisterUserResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="result">Результат регистрации</param>
    /// <param name="jwt">Jwt токен</param>
    public RegisterUserResponse(IdentityResult result, string jwt)
    {
        Result = result;
        Jwt = jwt;
    }

    /// <summary>
    /// Результат регистрации
    /// </summary>
    public IdentityResult Result { get; }

    /// <summary>
    /// Jwt токен
    /// </summary>
    public string Jwt { get; }
}