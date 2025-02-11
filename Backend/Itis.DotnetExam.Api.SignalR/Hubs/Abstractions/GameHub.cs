using Itis.DotnetExam.Api.SignalR.Events;
using Itis.DotnetExam.Api.SignalR.Models;

namespace Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;

/// <summary>
/// Хаб игры
/// </summary>
public interface IGameHub
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
    
    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task ReceiveMessage(SendMessageModel message);
}