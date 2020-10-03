using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base
{
    /// <summary>
    /// Таблица базы данных
    /// </summary>
    public interface IDatabaseTable<TId, TEntity>
        where TEntity : IEntityModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// Вернуть записи из таблицы асинхронно
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync();

        /// <summary>
        /// Вернуть записи из таблицы асинхронно с включением сущностей
        /// </summary>
        Task<IResultCollection<TEntity>> ToListAsync<TIdOut, TEntityOut>(Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TEntity>> FindAsync(TId id);

        /// <summary>
        /// Вернуть запись из таблицы по идентификатору асинхронно с включением сущностей
        /// </summary>
        Task<IResultValue<TEntity>> FindAsync<TIdOut, TEntityOut>(TId id, Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Найти записи в таблице по идентификаторам
        /// </summary>
        Task <IResultCollection<TEntity>> FindAsync(IEnumerable<TId> ids);

        /// <summary>
        /// Найти записи в таблице по идентификаторам с включением сущностей
        /// </summary>
        Task<IResultCollection<TEntity>> FindAsync<TIdOut, TEntityOut>(IEnumerable<TId> ids,
                                                                       Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Запрос поиска по идентификатору
        /// </summary>
        IQueryable<TEntity> Where(TId id);

        /// <summary>
        /// Запрос поиска по идентификаторам
        /// </summary>
        IQueryable<TEntity> Where(IEnumerable<TId> ids);

        /// <summary>
        /// Запрос поиска по идентификатору с включением сущностей
        /// </summary>
        IQueryable<TEntity> Where<TIdOut, TEntityOut>(TId id,
                                                      Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Запрос поиска по идентификаторам  с включением сущностей
        /// </summary>
        IQueryable<TEntity> Where<TIdOut, TEntityOut>(IEnumerable<TId> ids,
                                                      Expression<Func<TEntity, IEnumerable<TEntityOut>>> include)
            where TEntityOut : IEntityModel<TIdOut>
            where TIdOut : notnull;

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task<IResultCollection<TId>> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Добавить элемент в таблице
        /// </summary>
        IResultError Update(TEntity entity);

        /// <summary>
        /// Удалить элемент в таблице
        /// </summary>
        IResultValue<TEntity> Remove(TEntity entity);

        /// <summary>
        /// Удалить элементы в таблице
        /// </summary>
        IResultError RemoveRange(IEnumerable<TEntity> entities);
    }
}