using Itis.DotnetExam.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.Core.Abstractions;

/// <summary>
/// Контекст EF Core для приложения
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Игровое лобби
    /// </summary>
    public DbSet<Game> Games { get; set; }
    
    /// <summary>
    /// Игровое лобби
    /// </summary>
    public DbSet<Chat> Chats { get; set; }
    
    /// <summary>
    /// Игровое лобби
    /// </summary>
    public DbSet<Message> Message { get; set; }
    
    /// <summary>
    /// Сохранить изменения в БД
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество обновленных записей</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}