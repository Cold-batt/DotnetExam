namespace Itis.DotnetExam.Api.MediatR.Abstractions;

/// <summary>
/// Интерфейс для обработчиков запросов.
/// </summary>
/// <typeparam name="TQuery">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип результата запроса.</typeparam>
public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Обрабатывает запрос.
    /// </summary>
    /// <param name="query">Запрос для обработки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат выполнения запроса.</returns>
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}