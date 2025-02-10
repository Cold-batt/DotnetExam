namespace Itis.DotnetExam.Api.MediatR.Abstractions;

/// <summary>
/// Интерфейс для обработчиков команд с результатом.
/// </summary>
/// <typeparam name="TCommand">Тип команды.</typeparam>
/// <typeparam name="TResponse">Тип результата команды.</typeparam>
public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    /// <summary>
    /// Обрабатывает команду.
    /// </summary>
    /// <param name="command">Команда для обработки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат выполнения команды.</returns>
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Интерфейс для обработчиков команд без результата.
/// </summary>
/// <typeparam name="TCommand">Тип команды.</typeparam>
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Обрабатывает команду.
    /// </summary>
    /// <param name="command">Команда для обработки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task Handle(TCommand command, CancellationToken cancellationToken);
}