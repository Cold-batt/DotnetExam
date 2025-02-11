using Itis.DotnetExam.Api.Contracts.Requests.Chat;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.Chat.GetChat;

public class GetChatByGameIdQuery: IQuery<GetChatResponse>
{
    public Guid GameId { get; set; }
}