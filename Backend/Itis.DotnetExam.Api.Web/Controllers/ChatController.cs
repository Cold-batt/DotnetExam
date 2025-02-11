using Itis.DotnetExam.Api.Contracts.Requests.Chat;
using Itis.DotnetExam.Api.Core.Requests.Chat.GetChat;
using Itis.DotnetExam.Api.Core.Requests.Chat.SendMessage;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Itis.DotnetExam.Api.Web.Controllers;

/// <summary>
/// Контроллер чата
/// </summary>
public class ChatController : BaseController
{
    /// <summary>
    /// Получить список чатов
    /// </summary>
    [HttpGet]
    public async Task<GetChatResponse> GetChatByGameId(
        [FromServices] IMediator mediator,
        [FromQuery] Guid gameId,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetChatByGameIdQuery
        {
            GameId = gameId
        }, cancellationToken);

    /// <summary>
    /// Отправить сообщение в чат
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("sendMessage")]
    public async Task<ResponseMessage> SendMessage(
        [FromServices] IMediator mediator,
        [FromBody] SendMessageRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new SendMessageCommand
        {
            UserId = CurrentUserId,
            ChatId = request.ChatId,
            Text = request.Text,
        }, cancellationToken);
}