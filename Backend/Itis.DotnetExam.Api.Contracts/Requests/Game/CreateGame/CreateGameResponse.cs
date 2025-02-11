namespace Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;

/// <summary>
/// Ответ на запрос на создание игры
/// </summary>
public class CreateGameResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="gameId">Id созданной игры</param>
    public CreateGameResponse(Guid gameId)
    {
        GameId = gameId;
    }
    
    /// <summary>
    /// Id созданной игры
    /// </summary>
    public Guid GameId { get; set; }
}