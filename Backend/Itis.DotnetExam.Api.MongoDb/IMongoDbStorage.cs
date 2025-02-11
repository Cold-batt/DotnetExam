using Itis.DotnetExam.Api.MongoDb.Models;

namespace Itis.DotnetExam.Api.MongoDb;

/// <summary>
/// Сервис с круд операциями к MongoDb
/// </summary>
/// <typeparam name="TEntity">модель сущности</typeparam>
public interface IMongoDbStorage<TEntity> where TEntity : IMongoDbEntity
{
	/// <summary>
	/// Получить сущность
	/// </summary>
	/// <param name="id">id</param>
	/// <returns>null если не найден</returns>
	public Task<TEntity?> GetByIdAsync(Guid id);

	/// <summary>
	/// Вставить сушность
	/// </summary>
	/// <param name="entity">новая сущность</param>
	public Task InsertAsync(TEntity entity);

	/// <summary>
	/// Обновить сущность
	/// </summary>
	/// <param name="entity">обновленная сущность</param>
	public Task UpdateAsync(TEntity entity);

	/// <summary>
	/// Удалить сущность
	/// </summary>
	/// <param name="id">id</param>
	public Task DeleteAsync(Guid id);
}