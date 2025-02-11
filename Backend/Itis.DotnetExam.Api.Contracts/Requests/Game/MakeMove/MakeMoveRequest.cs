namespace Itis.DotnetExam.Api.Contracts.Requests.Game.MakeMove;

public class MakeMoveRequest
{
    public int Index { get; set; }

    public Guid UserId { get; set; }
}