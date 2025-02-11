using Itis.DotnetExam.Api.MongoDb.Models;
using MongoDB.Driver;

namespace Itis.DotnetExam.Api.MongoDb;

/// <summary>
/// Сервис с круд операциями к MongoDb
/// </summary>
/// <typeparam name="TEntity">модель сущности</typeparam>
public class MongoDbStorage<TEntity>(IMongoCollection<TEntity> mongoCollection) : IMongoDbStorage<TEntity>
	where TEntity : IMongoDbEntity
{
	public async Task<TEntity?> GetByIdAsync(Guid id) =>
		await mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

	public async Task InsertAsync(TEntity entity) =>
		await mongoCollection.InsertOneAsync(entity);

	public async Task UpdateAsync(TEntity entity) =>
		await mongoCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

	public async Task DeleteAsync(Guid id) =>
		await mongoCollection.DeleteOneAsync(x => x.Id == id);
}