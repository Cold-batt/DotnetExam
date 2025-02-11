using Itis.DotnetExam.Api.Contracts.Enums;
using Itis.DotnetExam.Api.Contracts.Requests.Chat;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Requests.Chat.GetChat;

public class GetChatByGameIdQueryHandler: IQueryHandler<GetChatByGameIdQuery, GetChatResponse>
{
    private readonly IDbContext _context;

    public GetChatByGameIdQueryHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<GetChatResponse> Handle(GetChatByGameIdQuery query, CancellationToken cancellationToken)
    {
        var chat = await _context.Chats
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(x => x.GameId == query.GameId, cancellationToken)
            ?? throw new NotFoundException("Не найден чат");
        
        return new GetChatResponse
        {
            ChatId = chat.Id,
            Messages = chat.Messages
                .Select(x => new ResponseMessage
                {
                    Id = x.Id,
                    ChatId = x.ChatId,
                    Text = x.Text,
                    UserName = x.UserName,
                    MessageType = MessageTypes.Default,
                    CreatedAt = x.CreatedAt
                })
                .OrderBy(x => x.CreatedAt)
                .ToList()
        };
    }
}