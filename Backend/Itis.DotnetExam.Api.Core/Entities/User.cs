using Itis.DotnetExam.Api.Core.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Itis.DotnetExam.Api.Core.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User: IdentityUser<Guid>, IEntity
{
}