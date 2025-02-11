using Itis.DotnetExam.Api.Contracts.Enums;

namespace Itis.DotnetExam.Api.Contracts.Models;

/// <summary>
/// Игровой
/// </summary>
public struct GameProgress
{
    public Guid? MoveUserId { get; set; }
    
    public MapMarkers[] GameMap { get; set; }
}