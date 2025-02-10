using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.MediatR;

/// <summary>
/// Реализация медиатора для обработки команд и запросов.
/// </summary>
public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="Mediator"/>.
    /// </summary>
    /// <param name="serviceProvider">Провайдер сервисов для разрешения зависимостей.</param>
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler not found for command type {command.GetType()}");
        }

        var handleMethod = handlerType.GetMethod("Handle");
        return (Task<TResponse>)handleMethod.Invoke(handler, new object[] { command, cancellationToken });
    }

    /// <inheritdoc />
    public Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler not found for command type {command.GetType()}");
        }

        var handleMethod = handlerType.GetMethod("Handle");
        return (Task)handleMethod.Invoke(handler, new object[] { command, cancellationToken });
    }

    /// <inheritdoc />
    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
        {
            throw new InvalidOperationException($"Handler not found for query type {query.GetType()}");
        }

        var handleMethod = handlerType.GetMethod("Handle");
        return (Task<TResponse>)handleMethod.Invoke(handler, new object[] { query, cancellationToken });
    }
}