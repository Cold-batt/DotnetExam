using Itis.DotnetExam.Api.MediatR.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Itis.DotnetExam.Api.MediatR;

/// <summary>
/// Класс, конфигурирующий медиатор
/// </summary>
public static class Entry
{
    /// <summary>
    /// Настроить медиатор
    /// </summary>
    public static IServiceCollection AddMediator(this IServiceCollection services, Action<IServiceCollection> configure)
    {
        services.AddSingleton<IMediator, Mediator>();
        
        configure(services);
        
        return services;
    }
}