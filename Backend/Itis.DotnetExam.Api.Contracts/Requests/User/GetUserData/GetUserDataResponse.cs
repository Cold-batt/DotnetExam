namespace Itis.DotnetExam.Api.Contracts.Requests.User.GetUserData;

/// <summary>
/// Ответ на получение информации о пользователе
/// </summary>
public class GetUserDataResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="username">Никнейм</param>
    /// <param name="userRaiting">Рейтинг пользователя</param>
    public GetUserDataResponse(string username, int userRaiting)
    {
        Username = username;
        UserRaiting = userRaiting;
    }
    
    /// <summary>
    /// Никнейм
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Рейтинг пользователя
    /// </summary>
    public int UserRaiting { get; set; }
}