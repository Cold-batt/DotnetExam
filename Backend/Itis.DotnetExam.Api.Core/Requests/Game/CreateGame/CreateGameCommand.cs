using Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.Game.CreateGame;

/// <summary>
/// Команда на создание игры
/// </summary>
public class CreateGameCommand : CreateGameRequest, ICommand<CreateGameResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Id пользователя (хоста)</param>
    public CreateGameCommand(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary>
    /// Id пользователя (хоста)
    /// </summary>
    public Guid UserId { get; }
}