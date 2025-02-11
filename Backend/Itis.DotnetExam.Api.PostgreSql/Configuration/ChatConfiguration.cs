using Itis.DotnetExam.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.DotnetExam.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация <see cref="Chat"/>
/// </summary>
internal class ChatConfiguration : EntityBaseConfiguration<Chat>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats", "public")
            .HasComment("Чаты");
        
        builder.HasOne(x => x.Game)
            .WithOne(x => x.Chat)
            .HasForeignKey<Chat>(x => x.GameId);

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Chat)
            .HasForeignKey(x => x.ChatId)
            .HasPrincipalKey(x => x.Id)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}