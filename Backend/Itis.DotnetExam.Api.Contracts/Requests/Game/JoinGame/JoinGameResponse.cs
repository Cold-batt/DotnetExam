using Itis.DotnetExam.Api.Contracts.Models;

namespace Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;

public class JoinGameResponse
{
    public Guid GameId { set; get; }
    public Guid? OwnerId { set; get; }
    public Guid? OpponentId { set; get; }
    public GameProgress GameProgress { set; get; }
}