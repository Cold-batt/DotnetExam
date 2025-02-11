using Itis.DotnetExam.Api.Contracts.Requests.Game.GetGames;
using Itis.DotnetExam.Api.Core.Models;
using Itis.DotnetExam.Api.MediatR.Abstractions;

namespace Itis.DotnetExam.Api.Core.Requests.Game.GetGames;

/// <summary>
/// Запрос получения списка игр
/// </summary>
public class GetGamesQuery : GetGamesRequest, IQuery<GetGamesResponse>, IPaginationQuery
{
}