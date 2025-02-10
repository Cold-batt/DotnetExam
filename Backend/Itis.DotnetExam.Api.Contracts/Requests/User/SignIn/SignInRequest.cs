using System.ComponentModel.DataAnnotations;

namespace Itis.DotnetExam.Api.Contracts.Requests.User.SignIn;

/// <summary>
/// Запрос на вход
/// </summary>
public class SignInRequest
{
    /// <summary>
    /// Ник пользователя
    /// </summary>
    [Required]
    public string UserName { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}