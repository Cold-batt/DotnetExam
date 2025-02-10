using Itis.DotnetExam.Api.Contracts.Requests.User.SignIn;

namespace Itis.DotnetExam.Api.Core.Requests.User.SignIn;

/// <summary>
/// Команда запроса <see cref="SignInRequest"/>
/// </summary>
public class SignInQuery: SignInRequest, IRequest<SignInResponse>
{
}