using Itis.DotnetExam.Api.SignalR.Enums;

namespace Itis.DotnetExam.Api.SignalR.Models;

/// <summary>
/// Модель отправки сообщения
/// </summary>
/// <param name="GameId"></param>
/// <param name="UserName"></param>
/// <param name="Message"></param>
public record SendMessageModel(Guid GameId, string UserName, string Message, MessageTypes MessageType);