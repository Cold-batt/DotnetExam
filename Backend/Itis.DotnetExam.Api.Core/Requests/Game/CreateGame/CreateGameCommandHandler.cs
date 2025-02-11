using Itis.DotnetExam.Api.Contracts.Enums;
using Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.MediatR.Abstractions;

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
        var map = Enumerable.Range(0, 9)
            .Select(_ => MapMarkers.Empty)
            .ToArray();
        
        var game = new Entities.Game(request.UserId, request.MaxRate, GameState.Created, map);

        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateGameResponse(game.Id);
    }
}