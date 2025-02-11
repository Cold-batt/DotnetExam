using Itis.DotnetExam.Api.Contracts.Requests.User.GetUserData;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.User.GetUserData;

/// <summary>
/// Запрос на получение информации о пользователе
/// </summary>
public class GetUserDataQuery : IQuery<GetUserDataResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    public GetUserDataQuery(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; }
}