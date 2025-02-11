using Itis.DotnetExam.Api.Contracts.Enums;

namespace Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;

/// <summary>
/// Запрос получения списка игр
/// </summary>
public class GetGamesRequest
{
    private int _pageNumber;
    private int _pageSize;
    private string? _orderBy;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetGamesRequest()
    {
        _pageNumber = PaginationDefaults.PageNumber;
        _pageSize = PaginationDefaults.PageSize;
    }

    /// <summary>
    /// Номер страницы, начиная с 1
    /// </summary>
    public int PageNumber { get => _pageNumber; set => _pageNumber = value > 0 ? value : PaginationDefaults.PageNumber; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get => _pageSize; set => _pageSize = value > 0 ? value : PaginationDefaults.PageSize; }

    /// <summary>
    /// Сортировка по возрастанию
    /// </summary>
    public bool IsAscending { get; set; }
}