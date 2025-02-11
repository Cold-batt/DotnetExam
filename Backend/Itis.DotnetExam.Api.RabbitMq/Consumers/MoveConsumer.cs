using Itis.DotnetExam.Api.Contracts.Enums;
using Itis.DotnetExam.Api.Contracts.Models;
using Itis.DotnetExam.Api.Contracts.Requests.Game.MakeMove;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Enums;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;
using Itis.DotnetExam.Api.SignalR.Events;
using Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.RabbitMq.Consumers;

public class MoveConsumer : IConsumer<MakeMoveRequest>
{
    private readonly IDbContext _dbContext;
    private readonly IMessageHandler _messageHandler;
    private readonly IMongoDbStorage<UserRating> _mongoDbStorage;

    public MoveConsumer(IDbContext dbContext, IMongoDbStorage<UserRating> mongoDbStorage, IMessageHandler messageHandler)
    {
        _dbContext = dbContext;
        _mongoDbStorage = mongoDbStorage;
        _messageHandler = messageHandler;
    }

    public async Task Consume(ConsumeContext<MakeMoveRequest> context)
    {
        var game = await _dbContext.Games
            .FirstOrDefaultAsync(x =>
                (x.OpponentId == context.Message.UserId || context.Message.UserId == x.OwnerId)
                && x.GameState != GameState.Finished);

        if (game is null) return;

        var isOwner = game.OwnerId == context.Message.UserId;
        var movesCount = game!.GameMap.Count(x => x != MapMarkers.Empty);
        var thisUserMovable = movesCount % 2 == 0 && isOwner || movesCount % 1 == 0 && !isOwner;

        if (!thisUserMovable) return;
        if (game.GameMap[context.Message.Index] != MapMarkers.Empty) return;

        game.GameMap[context.Message.Index] = isOwner ? MapMarkers.Circle : MapMarkers.Cross;

        var result = game.GameMap.CheckMap();

        if (result != MapMarkers.Empty)
        {
            await _messageHandler.GameFinish(
                new GameEvents.Finish(
                    game.Id,
                    game.GameMap,
                    result is null
                        ? null
                        : context.Message.UserId));

            game.GameState = GameState.Finished;

            await _dbContext.SaveChangesAsync();

            var winnerRating = await _mongoDbStorage.GetByIdAsync(context.Message.UserId)
                               ?? throw new ArgumentException(
                                   $"User rating for user with id: {context.Message.UserId} not found");
            var loserRating = await _mongoDbStorage.GetByIdAsync(game.OpponentId!.Value)
                              ?? throw new ArgumentException(
                                  $"User rating for user with id: {context.Message.UserId} not found");

            winnerRating.Rating += result is null ? -1 : 3;
            loserRating.Rating -= -1;

            await _mongoDbStorage.UpdateAsync(winnerRating);
            await _mongoDbStorage.UpdateAsync(loserRating);

            return;
        }

        var nextTurnUser = isOwner ? game.OwnerId : game.OpponentId;

        await _messageHandler.MoveMade(new GameEvents.Move(game.Id, game.GameMap, nextTurnUser!.Value));

        await _dbContext.SaveChangesAsync();
    }
}