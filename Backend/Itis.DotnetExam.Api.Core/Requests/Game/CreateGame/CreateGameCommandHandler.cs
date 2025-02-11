using Itis.DotnetExam.Api.Contracts.Enums;
using Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Requests.Game.CreateGame;

/// <summary>
/// Обработчик запроса для <see cref="CreateGameCommand"/>
/// </summary>
public class CreateGameCommandHandler : ICommandHandler<CreateGameCommand, CreateGameResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст бд</param>
    public CreateGameCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<CreateGameResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var oldGame = await _dbContext.Games
            .FirstOrDefaultAsync(x => x.OwnerId == request.UserId, cancellationToken);
        var oldChat = await _dbContext.Chats
            .FirstOrDefaultAsync(x => x.GameId == (oldGame == null ? Guid.Empty : oldGame.Id), cancellationToken);

        if (oldGame != null)
            _dbContext.Games.Remove(oldGame);
        
        if (oldChat != null)
            _dbContext.Chats.Remove(oldChat);
        
        var map = Enumerable.Range(0, 9)
            .Select(_ => MapMarkers.Empty)
            .ToArray();
        
        var game = new Entities.Game(request.UserId, request.MaxRate, GameState.Created, map);
        
        var chat = new Entities.Chat();
        
        game.Chat = chat;
        
        _dbContext.Games.Add(game);
        _dbContext.Chats.Add(chat);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        chat.Game = game;
        
        await _dbContext.SaveChangesAsync(cancellationToken);


        return new CreateGameResponse(game.Id);
    }
}