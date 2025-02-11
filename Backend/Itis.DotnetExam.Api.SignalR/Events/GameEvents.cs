using Itis.DotnetExam.Api.Contracts.Enums;

namespace Itis.DotnetExam.Api.SignalR.Events;

public static class GameEvents
{
    public record Start(Guid GameId, Guid Owner, Guid Opponent, Guid CurrentTurnId);

    public record Move(Guid GameId, MapMarkers[] Board, Guid CurrentTurnId);

    public record Finish(Guid GameId, MapMarkers[] Board, Guid? WinnerId);
}