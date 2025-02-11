using System.Globalization;
using System.Linq.Expressions;
using Itis.DotnetExam.Api.Core.Exceptions;
using Itis.DotnetExam.Api.Core.Models;

namespace Itis.DotnetExam.Api.Core.Extensions
{
	/// <summary>
	/// Расширения <see cref="IQueryable"/>
	/// </summary>
	public static class IQueryableExtensions
	{
		/// <summary>
		/// Применить пагинацию
		/// <see cref="IPaginationQuery"/>
		/// </summary>
		/// <typeparam name="T">Тип IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="pagination">Пагинация</param>
		/// <returns>IQueryable с пагинацией</returns>
		public static IQueryable<T> SkipTake<T>(this IQueryable<T> queryable, IPaginationQuery pagination)
		{
			ArgumentNullException.ThrowIfNull(queryable);

			if (pagination == null || pagination.PageNumber <= 0 || pagination.PageSize <= 0)
				return queryable;

			return queryable
				.Skip((pagination.PageNumber - 1) * pagination.PageSize)
				.Take(pagination.PageSize);
		}

		/// <summary>
		/// Применить сортировку
		/// <see cref="IPaginationQuery"/>
		/// </summary>
		/// <typeparam name="T">Тип IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="orderBy">Сортировка</param>
		/// <returns>IQueryable с сортировкой</returns>
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, IOrderByQuery orderBy)
		{
			ArgumentNullException.ThrowIfNull(queryable);

			if (string.IsNullOrWhiteSpace(orderBy?.OrderBy))
				return queryable;

			return queryable.OrderByField(orderBy.OrderBy, orderBy.IsAscending);
		}

		/// <summary>
		/// Применить сортировку
		/// <see cref="IPaginationQuery"/>
		/// </summary>
		/// <typeparam name="T">Тип IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="orderBy">Сортировка</param>
		/// <returns>IQueryable с сортировкой</returns>
		public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> queryable, IOrderByQuery orderBy)
		{
			ArgumentNullException.ThrowIfNull(queryable);

			if (string.IsNullOrWhiteSpace(orderBy?.OrderBy))
				return queryable;

			return queryable.ThenByField(orderBy.OrderBy, orderBy.IsAscending);
		}

		/// <summary>
		/// Сортировать по полю, указанному в текстовом виде
		/// Стащено отсюда: https://stackoverflow.com/a/22227975
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="propertyName">Поле для сортировки</param>
		/// <param name="isAscending">По возрастанию ли</param>
		/// <returns>IQueryable отсортированный</returns>
		public static IOrderedQueryable<T> OrderByField<T>(this IQueryable<T> queryable, string propertyName, bool isAscending = true)
		{
			ArgumentNullException.ThrowIfNull(queryable);

			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			var param = Expression.Parameter(queryable.ElementType, "p");
			var prop = (Expression)param;
			foreach (var property in propertyName.Split('.'))
				prop = ExtractProperty(prop, property);

			var exp = Expression.Lambda(prop, param);
			var method = isAscending ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);

			// PostgreSQL сортирует DateTime? через NULL FIRST по умолчанию, в какую бы сторону сортировка ни шла
			if (exp.ReturnType == typeof(DateTime?))
			{
				queryable = queryable.OrderByHasValue(propertyName, false);
				method = isAscending ? nameof(Queryable.ThenBy) : nameof(Queryable.ThenByDescending);
			}

			var types = new[] { queryable.ElementType, exp.Body.Type };
			var mce = Expression.Call(typeof(Queryable), method, types, queryable.Expression, exp);
#pragma warning disable CS8603 // Possible null reference return.
			return queryable.Provider.CreateQuery<T>(mce) as IOrderedQueryable<T>;
#pragma warning restore CS8603 // Possible null reference return.
		}

		/// <summary>
		/// Сортировать по полю, указанному в текстовом виде
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="propertyName">Поле для сортировки</param>
		/// <param name="isAscending">По возрастанию ли</param>
		/// <returns>IQueryable отсортированный</returns>
		public static IOrderedQueryable<T> ThenByField<T>(this IQueryable<T> queryable, string propertyName, bool isAscending = true)
		{
			if (queryable == null)
				throw new ArgumentNullException(nameof(queryable));

			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			var param = Expression.Parameter(queryable.ElementType, "p");
			var prop = (Expression)param;
			foreach (var property in propertyName.Split('.'))
				prop = ExtractProperty(prop, property);

			var exp = Expression.Lambda(prop, param);
			var method = isAscending ? nameof(Queryable.ThenBy) : nameof(Queryable.ThenByDescending);

			// PostgreSQL сортирует DateTime? через NULL FIRST по умолчанию, в какую бы сторону сортировка ни шла
			if (exp.ReturnType == typeof(DateTime?))
			{
				queryable = queryable.OrderByHasValue(propertyName, false);
			}

			var types = new[] { queryable.ElementType, exp.Body.Type };
			var mce = Expression.Call(typeof(Queryable), method, types, queryable.Expression, exp);
#pragma warning disable CS8603 // Possible null reference return.
			return queryable.Provider.CreateQuery<T>(mce) as IOrderedQueryable<T>;
#pragma warning restore CS8603 // Possible null reference return.
		}

		/// <summary>
		/// Сортировать по наличию свойства
		/// Стащено отсюда: https://stackoverflow.com/a/22227975
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <param name="queryable">IQueryable</param>
		/// <param name="propertyName">Поле для сортировки</param>
		/// <param name="isAscending">По возрастанию ли</param>
		/// <returns>IQueryable отсортированный</returns>
		// ReSharper disable once SuggestBaseTypeForParameter
		private static IOrderedQueryable<T> OrderByHasValue<T>(this IQueryable<T> queryable, string propertyName, bool isAscending = true)
		{
			if (queryable == null)
				throw new ArgumentNullException(nameof(queryable));

			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			var param = Expression.Parameter(typeof(T), "p");
			var prop = (Expression)param;
			foreach (var property in propertyName.Split('.'))
				prop = ExtractProperty(prop, property);
			prop = Expression.Property(prop, "HasValue");

			var exp = Expression.Lambda(prop, param);
			var method = isAscending ? "OrderBy" : "OrderByDescending";
			var types = new[] { queryable.ElementType, exp.Body.Type };
			var mce = Expression.Call(typeof(Queryable), method, types, queryable.Expression, exp);
#pragma warning disable CS8603 // Possible null reference return.
			return queryable.Provider.CreateQuery<T>(mce) as IOrderedQueryable<T>;
#pragma warning restore CS8603 // Possible null reference return.
		}

		/// <summary>
		/// Применить название свойства к выражению доступа к свойству
		/// </summary>
		/// <param name="expression">Выражение доступа к свойству</param>
		/// <param name="property">Свойство</param>
		/// <exception cref="InvalidOrderByExpressionException">Если не удалось преобразовать к свойству или полю</exception>
		/// <returns>Выражение со свойством или полем или исключение</returns>
		private static Expression ExtractProperty(Expression expression, string property)
		{
			try
			{
				return Expression.PropertyOrField(expression, property);
			}
			catch (ArgumentException)
			{
				throw new InvalidOrderByExpressionException();
			}
		}

		/// <summary>
		/// Условная фильтрация последовательности
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="condition">Проводить или нет фильтрацию</param>
		/// <param name="predicate">Предикат для отбора элементов</param>
		/// <returns>Отфильтрованное выражение</returns>
		public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
			=> condition ? query.Where(predicate) : query;

		/// <summary>
		/// Условие применимости ограничения фильтрации последовательности по массиву
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <typeparam name="TProperty">Тип списка</typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="condition">Условие</param>
		/// <param name="propertyExpression">Предикат для отбора элементов</param>
		/// <param name="array">Проверяемый массив элементов</param>
		/// <param name="returnEmpty">Признак возврата пустой колекции</param>
		/// <returns>Отфильтрованное выражение</returns>
		public static IQueryable<T> WhereIfContains<T, TProperty>(
			this IQueryable<T> query,
			bool condition,
			Expression<Func<T, TProperty>> propertyExpression,
			IEnumerable<TProperty> array,
			bool returnEmpty = true)
			=> condition ? query.WhereContains(propertyExpression, array, returnEmpty) : query;

		/// <summary>
		/// Фильтрация последовательности по массиву
		/// </summary>
		/// <typeparam name="T">Тип сущностей IQueryable</typeparam>
		/// <typeparam name="TProperty">Тип списка</typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="propertyExpression">Предикат для отбора элементов</param>
		/// <param name="array">Проверяемый массив элементов</param>
		/// <param name="returnEmpty">Признак возврата пустой колекции</param>
		/// <returns>Отфильтрованное выражение</returns>
		public static IQueryable<T> WhereContains<T, TProperty>(
			this IQueryable<T> query,
			Expression<Func<T, TProperty>> propertyExpression,
			IEnumerable<TProperty>? array,
			bool returnEmpty = true)
		{
			if (array == null)
				return query;

			if (!array.Any())
				return returnEmpty ? query.Where(x => false) : query;

			var parameterExpression = propertyExpression.Parameters[0];
			var expression = Expression.Call(Expression.Constant(array), array.GetType().GetMethod("Contains"), propertyExpression.Body);
			var predicate = Expression.Lambda<Func<T, bool>>(expression, parameterExpression);

			return query.Where(predicate);
		}

		/// <summary>
		/// Отфильтровать строку по включению без учёта регистра, если <paramref name="filterValue"/> не равен null
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="source">Запрос</param>
		/// <param name="filterValue">Значение для фильтрации</param>
		/// <param name="selector">Путь к полю</param>
		/// <returns>Отфильтрованный запрос</returns>
		public static IQueryable<T> Filter<T>(
			this IQueryable<T> source,
			string? filterValue,
			Expression<Func<T, string>> selector)
		{
			ArgumentNullException.ThrowIfNull(selector);

			if (filterValue is null)
				return source;

			var preparedValue = filterValue.ToLower(CultureInfo.InvariantCulture);

			var containsMethodInfo = typeof(string).GetMethod(nameof(string.Contains), new Type[]
			{
				typeof(string),
			});
			var toLowerMethodInfo = typeof(string).GetMethod(nameof(string.ToLower), Array.Empty<Type>());

			var parameterExpression = selector.Parameters[0];
			var expression = Expression.Call(selector.Body, toLowerMethodInfo!);
			expression = Expression.Call(expression, containsMethodInfo!, Expression.Constant(preparedValue));
			var predicate = Expression.Lambda<Func<T, bool>>(expression, new[] { parameterExpression });

			return source.Where(predicate);
		}

		/// <summary>
		/// Отфильтровать Guid, если <paramref name="filterValue"/> не равен null
		/// </summary>
		/// <typeparam name="T">Тип сущности</typeparam>
		/// <param name="source">Запрос</param>
		/// <param name="filterValue">Значение для фильтрации</param>
		/// <param name="selector">Путь к полю</param>
		/// <returns>Отфильтрованный запрос</returns>
		public static IQueryable<T> Filter<T>(
			this IQueryable<T> source,
			Guid? filterValue,
			Expression<Func<T, Guid>> selector)
		{
			ArgumentNullException.ThrowIfNull(selector);

			if (filterValue is null)
				return source;

			var equalMethodInfo = typeof(Guid).GetMethod(nameof(Guid.Equals), new Type[]
			{
				typeof(Guid),
			});

			var parameterExpression = selector.Parameters[0];
			var expression = Expression.Call(selector.Body, equalMethodInfo!, Expression.Constant(filterValue.Value));
			var predicate = Expression.Lambda<Func<T, bool>>(expression, new[] { parameterExpression });

			return source.Where(predicate);
		}
	}
}
