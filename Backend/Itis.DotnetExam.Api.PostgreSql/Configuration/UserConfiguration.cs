﻿using Itis.DotnetExam.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.DotnetExam.Api.PostgreSql.Configuration;

/// <summary>
/// Конфигурация для <see cref="User"/>>
/// </summary>
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";

    /// <summary>
    /// Конфигурация сущности
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "public")
            .HasComment("Профили пользователей");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasDefaultValueSql(GuidCommand);

        builder.Property(p => p.UserName)
            .HasComment("Никнейм пользователя");
        
        builder.Property(p => p.Email)
            .HasComment("Почта");
        
        builder.HasOne(x => x.OwnerGame)
            .WithOne(x => x.Owner)
            .HasForeignKey<Game>(x => x.OwnerId)
            .HasPrincipalKey<User>(x => x.Id);

        builder.HasOne(x => x.OpponentGame)
            .WithOne(x => x.Opponent)
            .HasForeignKey<Game>(x => x.OpponentId)
            .HasPrincipalKey<User>(x => x.Id);
    }
}