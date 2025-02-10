using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;

namespace Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;

/// <summary>
/// Команда запроса <see cref="RegisterUserRequest"/>
/// </summary>
public class RegisterUserCommand: RegisterUserRequest, IRequest<RegisterUserResponse>
{
}