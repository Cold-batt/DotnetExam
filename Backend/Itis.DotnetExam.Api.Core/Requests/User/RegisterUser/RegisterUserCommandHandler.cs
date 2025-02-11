using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;

namespace Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;

/// <summary>
/// Обработчик запроса <see cref="RegisterUserCommand"/>
/// </summary>
public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserService _userService;
    private readonly IMongoDbStorage<UserRating> _mongoDbStorage;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="mongoDbStorage">Монга</param>
    /// <param name="jwtService">Сервси JWT</param>
    public RegisterUserCommandHandler(
        IUserService userService,
        IMongoDbStorage<UserRating> mongoDbStorage,
        IJwtService jwtService)
    {
        _userService = userService;
        _mongoDbStorage = mongoDbStorage;
        _jwtService = jwtService;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> Handle(
        RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var user = new Entities.User
        {
            UserName = request.UserName,
        };
        
        var result = await _userService.RegisterUserAsync(user, request.Password);

         if (!result.Succeeded)
             throw new ApplicationExceptionBase(
                 $"Не удалось зарегистрировать пользователя: {string.Join('\n', result.Errors.Select(x => x.Description))}");
         
         var rating = new UserRating
         {
             Id = user.Id,
             Rating = 0
         };

         var jwt = _jwtService.GenerateJwt(user.Id, user.UserName);

        await _mongoDbStorage.InsertAsync(rating);
        
        return new RegisterUserResponse(result, jwt);
    }
}