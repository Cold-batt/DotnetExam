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
    /// <inheritdoc />
	public async Task<TEntity?> GetByIdAsync(Guid id) =>
		await mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    /// <inheritdoc />
	public async Task InsertAsync(TEntity entity) =>
		await mongoCollection.InsertOneAsync(entity);

    /// <inheritdoc />
	public async Task UpdateAsync(TEntity entity) =>
		await mongoCollection.ReplaceOneAsync(x => x.Id == entity.Id, entity);

    /// <inheritdoc />
	public async Task DeleteAsync(Guid id) =>
		await mongoCollection.DeleteOneAsync(x => x.Id == id);
}