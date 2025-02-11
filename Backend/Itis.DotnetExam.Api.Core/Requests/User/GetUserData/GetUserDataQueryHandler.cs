using Itis.DotnetExam.Api.Contracts.Requests.User.GetUserData;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.Core.Requests.Game.GetGames;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Requests.User.GetUserData;

/// <summary>
/// Обработчик запроса для <see cref="GetUserDataQuery"/>
/// </summary>
public class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, GetUserDataResponse>
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;
    private readonly IMongoDbStorage<UserRating> _mongoDbStorage;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userManager">Сервси для работы с пользователем</param>
    /// <param name="mongoDbStorage">Монго БД</param>
    public GetUserDataQueryHandler(
        UserManager<IdentityUser<Guid>> userManager,
        IMongoDbStorage<UserRating> mongoDbStorage)
    {
        _userManager = userManager;
        _mongoDbStorage = mongoDbStorage;
    }

    /// <inheritdoc />
    public async Task<GetUserDataResponse> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var username = await _userManager.Users
           .Where(x => x.Id == request.UserId)
           .Select(x => x.UserName)
           .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        var userRating = await _mongoDbStorage
            .GetByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        return new GetUserDataResponse(username, userRating.Rating);
    }
}