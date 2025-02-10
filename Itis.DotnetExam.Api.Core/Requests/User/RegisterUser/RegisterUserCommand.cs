using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;

/// <summary>
/// Команда запроса <see cref="RegisterUserRequest"/>
/// </summary>
public class RegisterUserCommand: RegisterUserRequest, ICommand<RegisterUserResponse>
{
}