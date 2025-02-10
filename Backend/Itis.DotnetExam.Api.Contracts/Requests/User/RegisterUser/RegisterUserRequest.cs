using System.ComponentModel.DataAnnotations;

namespace Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;

/// <summary>
/// Запрос на регистрацию пользователя
/// </summary>
public class RegisterUserRequest
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