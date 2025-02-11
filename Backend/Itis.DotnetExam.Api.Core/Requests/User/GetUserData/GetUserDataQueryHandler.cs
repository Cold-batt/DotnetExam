using Itis.DotnetExam.Api.Contracts.Requests.User.GetUserData;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;

namespace Itis.DotnetExam.Api.Core.Requests.User.GetUserData;

/// <summary>
/// Обработчик запроса для <see cref="GetUserDataQuery"/>
/// </summary>
public class GetUserDataQueryHandler : IQueryHandler<GetUserDataQuery, GetUserDataResponse>
{
    private readonly IUserService _userService;
    private readonly IMongoDbStorage<UserRating> _mongoDbStorage;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userManager">Сервси для работы с пользователем</param>
    /// <param name="mongoDbStorage">Монго БД</param>
    public GetUserDataQueryHandler(
        IUserService userManager,
        IMongoDbStorage<UserRating> mongoDbStorage)
    {
        _userService = userManager;
        _mongoDbStorage = mongoDbStorage;
    }

    /// <inheritdoc />
    public async Task<GetUserDataResponse> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        var userRating = await _mongoDbStorage
            .GetByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        return new GetUserDataResponse(user?.UserName ?? string.Empty, userRating.Rating);
    }
}