using Itis.DotnetExam.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.DotnetExam.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="Game"/>>
/// </summary>
internal class GameConfiguration : IEntityTypeConfiguration<Game>
{
    /// <summary>
    /// Конфигурация сущности
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("games", "public")
            .HasComment("Игровое лобби");

        builder.Property(p => p.GameState)
            .HasComment("Статус игры");
        
        builder.Property(p => p.OpponentId)
            .HasComment("Id оппонента");
        
        builder.Property(p => p.OwnerId)
            .HasComment("Id хоста");
        
        builder.Property(p => p.GameMap)
            .HasComment("Игровая карта");
        
        builder.Property(p => p.MaxRate)
            .HasComment("Максимальный рейтинг");
        
        builder.HasOne(x => x.Chat)
            .WithOne(x => x.Game)
            .HasForeignKey<Game>(x => x.ChatId);

        builder.HasOne(x => x.Owner)
            .WithOne(x => x.OwnerGame)
            .HasForeignKey<Game>(x => x.OwnerId)
            .HasPrincipalKey<User>(x => x.Id);

        builder.HasOne(x => x.Opponent)
            .WithOne(x => x.OpponentGame)
            .HasForeignKey<Game>(x => x.OpponentId)
            .HasPrincipalKey<User>(x => x.Id);
    }
}