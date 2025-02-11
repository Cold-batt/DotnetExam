using Itis.DotnetExam.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.DotnetExam.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация сообщения
/// </summary>
internal class MessageConfiguration : EntityBaseConfiguration<Message>
{
    /// <inheritdoc />
    protected override void ConfigureChild(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages", "public")
            .HasComment("Сообщения");

        builder.Property(x => x.Text)
            .HasComment("Текст сообщения");

        builder.Property(x => x.UserName)
            .HasComment("Пользователь");

        builder.Property(x => x.CreatedAt)
            .HasComment("дата");

        builder.HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId)
            .HasPrincipalKey(x => x.Id);
    }
}