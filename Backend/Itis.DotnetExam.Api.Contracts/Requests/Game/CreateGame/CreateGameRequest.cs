namespace Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;

/// <summary>
/// Запрос на создание игры
/// </summary>
public class CreateGameRequest
{
    /// <summary>
    /// Максимальный рейтинг
    /// </summary>
    public int MaxRate { get; set; }
}