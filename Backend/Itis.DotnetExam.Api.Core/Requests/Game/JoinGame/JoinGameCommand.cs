using Itis.DotnetExam.Api.Contracts.Requests.Game.JoinGame;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.Game.JoinGame;

public class JoinGameCommand: JoinGameRequest, ICommand<JoinGameResponse>
{
}