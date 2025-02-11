using Itis.DotnetExam.Api.Contracts.Enums;

namespace Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;

/// <summary>
/// Игра для <see cref="GetGamesResponse"/>
/// </summary>
public class GetGamesResponseItem
{
    /// <summary>
    /// Id игры
    /// </summary>
    public Guid GameId { get; set; }
    
    /// <summary>
    /// Максимальный рейтинг
    /// </summary>
    public int MaxRate { get; set; }
    
    /// <summary>
    /// Состояние игры
    /// </summary>
    public GameState GameState { get; set; }
}