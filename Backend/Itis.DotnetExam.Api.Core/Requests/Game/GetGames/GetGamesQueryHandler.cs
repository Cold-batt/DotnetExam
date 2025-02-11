using Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Extensions;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Requests.Game.GetGames;

/// <summary>
/// Обработчик запроса <see cref="GetGamesQuery"/>
/// </summary>
public class GetGamesQueryHandler : IQueryHandler<GetGamesQuery, GetGamesResponse>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public GetGamesQueryHandler(IDbContext dbContext)
        => _dbContext = dbContext;
    
    /// <inheritdoc />
    public async Task<GetGamesResponse> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Games;

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .Select(x => new GetGamesResponseItem()
            {
                GameId = x.Id,
                GameState = x.GameState,
                MaxRate = x.MaxRate,
            })
            .SkipTake(request)
            .ToListAsync(cancellationToken);

        return new GetGamesResponse(result, totalCount);
    }
}