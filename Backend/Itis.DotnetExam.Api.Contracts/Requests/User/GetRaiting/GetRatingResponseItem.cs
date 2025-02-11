namespace Itis.DotnetExam.Api.Contracts.Requests.User;

/// <summary>
/// Рейтинг для <see cref="GetRatingResponse"/>
/// </summary>
public class GetRatingResponseItem
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Рейтинг
    /// </summary>
    public int Rating { get; set; }
}