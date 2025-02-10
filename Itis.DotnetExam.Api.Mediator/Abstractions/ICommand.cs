namespace Itis.DotnetExam.Api.MediatR.Abstractions;

/// <summary>
/// Базовый интерфейс для команд (без результата).
/// </summary>
public interface ICommand { }

/// <summary>
/// Базовый интерфейс для команд с результатом.
/// </summary>
/// <typeparam name="TResponse">Тип результата команды.</typeparam>
public interface ICommand<TResponse> { }
