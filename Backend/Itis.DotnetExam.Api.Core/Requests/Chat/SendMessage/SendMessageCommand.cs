using Itis.DotnetExam.Api.Contracts.Requests.Chat;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.Chat.SendMessage;

public class SendMessageCommand : SendMessageRequest, ICommand<ResponseMessage>
{
    public Guid UserId { get; set; }
}