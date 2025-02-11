using Itis.DotnetExam.Api.SignalR.Events;

namespace Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;

/// <summary>
/// Хаб игры
/// </summary>
public interface ITicTacToeHub
{
    /// <summary>
    /// Начало игры
    /// </summary>
    public Task GameStarted(GameEvents.Start @event);
    
    /// <summary>
    /// Сделать ход
    /// </summary>
    public Task MoveMade(GameEvents.Move @event);
    
    /// <summary>
    /// Закончить игру
    /// </summary>
    public Task GameFinish(GameEvents.Finish @event);
}