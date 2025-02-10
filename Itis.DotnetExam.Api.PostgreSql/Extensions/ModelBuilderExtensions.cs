using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Itis.DotnetExam.Api.PostgreSql.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Конфигурация моделей при запуске
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<IdentityRole<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
    }
}