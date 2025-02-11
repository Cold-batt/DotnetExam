namespace Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;

public class JoinGameRequest
{
    public Guid GameId { set; get; }
    public Guid UserId { set; get; }
}
