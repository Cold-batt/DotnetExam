using Itis.DotnetExam.Api.Contracts.Requests.User;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Requests.Game.GetGames;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;

namespace Itis.DotnetExam.Api.Core.Requests.User.GetRating;

/// <summary>
/// Обработчки для <see cref="GetRatingQuery"/>
/// </summary>
public class GetRatingQueryHandler : IQueryHandler<GetRatingQuery, GetRatingResponse>
{
    private readonly IMongoDbStorage<UserRating> _mongoDbStorage;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mongoDbStorage">Бд монго</param>
    public GetRatingQueryHandler(        
        IMongoDbStorage<UserRating> mongoDbStorage)
    {
        _mongoDbStorage = mongoDbStorage;
    }
    
    /// <inheritdoc />
    public Task<GetRatingResponse> Handle(GetRatingQuery query, CancellationToken cancellationToken)
    {
        // TODO: Допилить таблицу рейтинга

        throw new NotImplementedException("Таблица рейтинга");
    }
}