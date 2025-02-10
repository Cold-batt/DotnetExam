namespace Itis.DotnetExam.Api.MediatR.Abstractions;

/// <summary>
/// Интерфейс медиатора для отправки команд и запросов.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Отправляет команду с результатом.
    /// </summary>
    /// <typeparam name="TResponse">Тип результата команды.</typeparam>
    /// <param name="command">Команда для выполнения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат выполнения команды.</returns>
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отправляет команду без результата.
    /// </summary>
    /// <param name="command">Команда для выполнения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task Send(ICommand command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отправляет запрос.
    /// </summary>
    /// <typeparam name="TResponse">Тип результата запроса.</typeparam>
    /// <param name="query">Запрос для выполнения.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат выполнения запроса.</returns>
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}