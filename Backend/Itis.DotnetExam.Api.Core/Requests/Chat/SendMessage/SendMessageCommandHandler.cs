using Itis.DotnetExam.Api.Contracts.Requests.Chat;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Entities;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Requests.Chat.SendMessage;

public class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, ResponseMessage>
{
    private readonly IDbContext _dbContext;
    private readonly IUserService _userService;

    public SendMessageCommandHandler(IDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task<ResponseMessage> Handle(SendMessageCommand command, CancellationToken cancellationToken)
    {
        var chat = await _dbContext.Chats
            .FirstOrDefaultAsync(x => x.Id == command.ChatId, cancellationToken);
        
        var user = await _userService.FindUserByIdAsync(command.UserId);

        var message = new Message(command.Text, DateTime.Now, chat)
        {
            UserName = user?.UserName ?? string.Empty,
        };

        _dbContext.Message.Add(message);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseMessage
        {
            Id = message.Id,
            ChatId = message.ChatId,
            UserName = message.UserName,
            CreatedAt = message.CreatedAt,
            Text = message.Text,
        };
    }
}