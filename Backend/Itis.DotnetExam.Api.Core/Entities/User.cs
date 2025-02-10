using Itis.DotnetExam.Api.Core.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Itis.DotnetExam.Api.Core.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User: IdentityUser<Guid>, IEntity
{
    /// <summary>
    /// Игра, где пользователь хост
    /// </summary>
    public Guid? OwnerGameId { get; set; }
    
    /// <summary>
    /// Игра, где пользователь оппонент
    /// </summary>
    public Guid? OpponentGameId { get; set; }

    #region Navigation properties

    /// <summary>
    /// Игра, где пользователь хост
    /// </summary>
    public Game? OwnerGame { get; set; }
    
    /// <summary>
    /// Игра, где пользователь оппонент
    /// </summary>
    public Game? OpponentGame { get; set; }

    #endregion
}