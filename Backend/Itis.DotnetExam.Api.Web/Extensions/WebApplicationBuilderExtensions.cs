using System.Reflection;
using System.Text;
using Itis.DotnetExam.Api.Contracts.Requests.Chat;
using Itis.DotnetExam.Api.Contracts.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;
using Itis.DotnetExam.Api.Contracts.Requests.Game;
using Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.Contracts.Requests.User.GetUserData;
using Itis.DotnetExam.Api.Contracts.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Contracts.Requests.User.SignIn;
using Itis.DotnetExam.Api.Web.Constants;
using Itis.DotnetExam.Api.Web.Middlewares;
using Itis.DotnetExam.Api.PostgreSql;
using Itis.DotnetExam.Api.Core.Abstractions;
using Itis.DotnetExam.Api.Core.Entities;
using Itis.DotnetExam.Api.Core.Requests.Chat.GetChat;
using Itis.DotnetExam.Api.Core.Requests.Chat.SendMessage;
using Itis.DotnetExam.Api.Core.Requests.Game.CreateGame;
using Itis.DotnetExam.Api.Core.Requests.Game.GetGames;
using Itis.DotnetExam.Api.Core.Requests.Game;
using Itis.DotnetExam.Api.Core.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.Core.Requests.User.GetUserData;
using Itis.DotnetExam.Api.Core.Requests.User.RegisterUser;
using Itis.DotnetExam.Api.Core.Requests.User.SignIn;
using Itis.DotnetExam.Api.Core.Services;
using Itis.DotnetExam.Api.MediatR;
using Itis.DotnetExam.Api.MediatR.Abstractions;
using Itis.DotnetExam.Api.MongoDb;
using Itis.DotnetExam.Api.MongoDb.Models;
using Itis.DotnetExam.Api.RabbitMq.Consumers;
using Itis.DotnetExam.Api.SignalR.Hubs;
using Itis.DotnetExam.Api.SignalR.Hubs.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace Itis.DotnetExam.Api.Web.Extensions;

/// <summary>
/// Класс с расширениями для <see cref="WebApplicationBuilder"/>
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Создание и настройка подключения к бд
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigurePostgresqlConnection(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<EfContext>(
            options =>
            {
                options.UseNpgsql(
                    builder.Configuration["Application:DbConnectionString"],
                    opt =>
                    {
                        opt.MigrationsAssembly(typeof(EfContext).GetTypeInfo().Assembly.GetName().Name);
                        opt.EnableRetryOnFailure(
                            15,
                            TimeSpan.FromSeconds(30),
                            null);
                    });
            });
    }

    /// <summary>
    /// Добавить МонгоДБ
    /// </summary>
    /// <param name="builder"></param>
    public static void ConfigureMongoDbConnection(this WebApplicationBuilder builder)
    {
        var client = new MongoClient(builder.Configuration["Application:MongoDbConnectionString"]);
        var database = client.GetDatabase("main");

        builder.Services.AddSingleton<IMongoDbStorage<UserRating>>(
            new MongoDbStorage<UserRating>(
                database.GetCollection<UserRating>(nameof(UserRating))));
    }

    /// <summary>
    /// Конфигурация кролика через masstransit
    /// </summary>
    public static void ConfigureRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(config =>
        {
            config.AddConsumer<MoveConsumer>();

            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(builder.Configuration["Application:RabbitMqConnectionString"]);
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
    
    /// <summary>
    /// Добавить службы и зависимости проекта
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureCore(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureMediator();
        
        builder.Services.AddScoped<IDbContext, EfContext>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IMessageHandler, MessageHandler>();
        builder.Services.AddSingleton<IJwtService, JwtService>();
        builder.Services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<EfContext>()
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromMinutes(5));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomSwagger();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: SpecificOrigins.MyAllowSpecificOrigins, 
                builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
        });
        builder.Services.AddSignalR();
    }

    /// <summary>
    /// Добавить и настроить авторизацию
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthorization(
                opt =>
                {
                    opt.DefaultPolicy = 
                        new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();
                });
    }

    /// <summary>
    /// Подключение и настройка JwtBearer
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureJwtBearer(this WebApplicationBuilder builder)
    {
        var issuer = builder.Configuration["JwtSettings:Issuer"];
        var audience = builder.Configuration["JwtSettings:Audience"];
        var secretKey = builder.Configuration["JwtSettings:SecretKey"]!;

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                };
            });
    }
    
    /// <summary>
    /// Использовать обработчик исключений.
    /// </summary>
    /// <param name="builder">Билдер пайплайна ASP.NET Core</param>
    /// <returns></returns>
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlingMiddleware>();

    /// <summary>
    /// Конфигурация медиатора
    /// </summary>
    private static IServiceCollection ConfigureMediator(this IServiceCollection services)
    {
        services.AddMediator(s =>
        {
            s.AddScoped<ICommandHandler<RegisterUserCommand, RegisterUserResponse>, RegisterUserCommandHandler>();
            s.AddScoped<ICommandHandler<JoinGameCommand, JoinGameResponse>, JoinGameCommandHandler>();
            s.AddScoped<ICommandHandler<CreateGameCommand, CreateGameResponse>, CreateGameCommandHandler>();
            s.AddScoped<ICommandHandler<SendMessageCommand, ResponseMessage>, SendMessageCommandHandler>();
            
            s.AddScoped<IQueryHandler<SignInQuery, SignInResponse>, SignInQueryHandler>();
            s.AddScoped<IQueryHandler<GetGamesQuery, GetGamesResponse>, GetGamesQueryHandler>();
            s.AddScoped<IQueryHandler<GetUserDataQuery, GetUserDataResponse>, GetUserDataQueryHandler>();
            s.AddScoped<IQueryHandler<GetChatByGameIdQuery, GetChatResponse>, GetChatByGameIdQueryHandler>();
        });

        return services;
    }
    
    private static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        => services
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
}