namespace Itis.DotnetExam.Api.PostgreSql.Constants;

/// <summary>
/// Идентификаторы ролей
/// </summary>
public static class RoleIds
{
    /// <summary>
    /// Администратор
    /// </summary>
    public static Guid Administrator = new ("3a96e520-caf4-464d-85e8-304863711e7b");
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public static Guid User = new ("eedd2ec5-1b1d-4ba7-9001-16db15898319");
}