using Itis.DotnetExam.Api.Contracts.Requests.User.SignIn;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.User.SignIn;

/// <summary>
/// Команда запроса <see cref="SignInRequest"/>
/// </summary>
public class SignInQuery: SignInRequest, IQuery<SignInResponse>
{
}