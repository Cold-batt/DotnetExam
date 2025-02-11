using System.ComponentModel.DataAnnotations;
using Itis.DotnetExam.Api.Contracts.Models;
using Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Enums;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;
using Itis.DotnetExam.Api.SignalR.Events;
using Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Requests.Game.JoinGame;

public class JoinGameCommandHandler : ICommandHandler<JoinGameCommand, JoinGameResponse>
{
    private readonly IDbContext _dbContext;
    private IMongoDbStorage<UserRating> _mongoDbStorage;
    private readonly IMessageHandler _eventMessageHandler;

    public JoinGameCommandHandler(
        IDbContext dbContext,
        IMessageHandler eventMessageHandler,
        IMongoDbStorage<UserRating> mongoDbStorage)
    {
        _dbContext = dbContext;
        _eventMessageHandler = eventMessageHandler;
        _mongoDbStorage = mongoDbStorage;
    }
    
    public async Task<JoinGameResponse> Handle(JoinGameCommand command, CancellationToken cancellationToken)
    {

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == command.GameId,
            cancellationToken: cancellationToken);
        
        if (game is null) 
            throw new NotFoundException("Игра не найдена");
        
        if (game.OpponentId == game.OwnerId)
            throw new ApplicationExceptionBase("OwnerId и OpponentId должны различаться");

        if (game.OpponentId is not null)
            return new JoinGameResponse
            {
                GameId = game.Id,
                OwnerId = game.OwnerId,
                OpponentId = game.OpponentId,
                GameProgress = new GameProgress
                {
                    GameMap = game.GameMap,
                    MoveUserId = game.OpponentId
                }
            };
        
        var userRating = await _mongoDbStorage.GetByIdAsync(command.UserId);
        if (userRating?.Rating > game.MaxRate)
            throw new ValidationException($"Максимальный рейтинг для этой игры {game.MaxRate}");

        game.OpponentId = command.UserId;
        game.GameState = GameState.Started;

        var userGames = _dbContext.Games
            .Where(x => x.OpponentId == command.UserId || x.OwnerId == command.UserId);

        foreach (var userGame in userGames)
        {
            if (userGame.OpponentId == command.UserId) userGame.OpponentId = null;
            if (userGame.OwnerId == command.UserId) _dbContext.Games.Remove(userGame);
        }
			
        await _eventMessageHandler.GameStarted(new GameEvents.Start(game.Id, game.OwnerId, game.OpponentId.Value, game.OpponentId.Value));
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new JoinGameResponse
        {
            GameId = game.Id,
            OwnerId = game.OwnerId,
            OpponentId = game.OpponentId,
            GameProgress = new GameProgress
            {
                GameMap = game.GameMap,
                MoveUserId = game.OpponentId
            }
        };
    }
}