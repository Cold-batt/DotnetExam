using Itis.DotnetExam.Api.Core.Abstractions;

namespace Itis.DotnetExam.Api.Core.Entities;

public class EntityBase : IEntity
{
    /// <summary>
    /// ИД сущности
    /// </summary>
    public Guid Id { get; set; }
}