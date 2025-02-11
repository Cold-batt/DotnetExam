namespace Itis.DotnetExam.Api.Contracts.Requests.User;

/// <summary>
/// Запрос на получение таблицы рейтинга
/// </summary>
public class GetRatingResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="items">Рейтинги</param>
    public GetRatingResponse(List<GetRatingResponseItem> items)
    {
        Items = items;
    }
    
    /// <summary>
    /// Рейтинги
    /// </summary>
    public List<GetRatingResponseItem> Items { get; }
}