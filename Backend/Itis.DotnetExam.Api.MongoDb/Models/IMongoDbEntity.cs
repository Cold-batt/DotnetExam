using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Itis.DotnetExam.Api.MongoDb.Models;

/// <summary>
/// Контракт для сущности MongoDb
/// </summary>
public interface IMongoDbEntity
{
	/// <summary>
	/// Id записи
	/// </summary>
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public Guid Id { get; init; }
}