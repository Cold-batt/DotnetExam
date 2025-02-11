using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Itis.DotnetExam.Api.MongoDb.Models;

/// <summary>
/// Рейтинг пользователя
/// </summary>
public class UserRating : IMongoDbEntity
{
	/// <summary>
	/// Id пользователя
	/// </summary>
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public required Guid Id { get; init; }

	/// <summary>
	/// Рейтинг пользователя
	/// </summary>
	public required int Rating { get; set; }
}