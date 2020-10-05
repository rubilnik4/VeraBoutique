﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultError.ResultErrorTryExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable
{
    /// <summary>
    /// Таблица базы данных EntityFramework. Функции добавления, обновления и удаления данных
    /// </summary>
    public abstract partial class EntityDatabaseTable<TId, TEntity>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        public async Task<IResultCollection<TId>> AddRangeAsync(IEnumerable<TEntity> entities) =>
            await ResultErrorTryAsync(() => _databaseSet.AddRangeAsync(entities), TableAccessError).
                  ToResultValueTaskAsync(entities.Select(entity => entity.Id)).
                  ToResultCollectionTaskAsync();

        /// <summary>
        /// Обновить элемент в таблице
        /// </summary>
        public new IResultError Update(TEntity entity) =>
            ResultErrorTry(() => _databaseSet.Update(entity), TableAccessError);

        /// <summary>
        /// Удалить элемент в таблице
        /// </summary>
        public new IResultValue<TEntity> Remove(TEntity entity) =>
            ResultValueTry(() => _databaseSet.Remove(entity).Entity, TableAccessError);

        /// <summary>
        /// Удалить элементы в таблице
        /// </summary>
        public new IResultError RemoveRange(IEnumerable<TEntity> entities) =>
            ResultErrorTry(() => _databaseSet.RemoveRange(entities), TableAccessError);
    }
}