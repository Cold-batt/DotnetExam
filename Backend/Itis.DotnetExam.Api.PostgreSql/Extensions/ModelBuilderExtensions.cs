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
    }
}