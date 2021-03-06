﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
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
    public abstract partial class EntityDatabaseTable<TId, TDomain, TEntity>
        where TDomain : IDomainModel<TId>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        public async Task<IResultValue<TId>> AddAsync(TEntity entity) =>
            await ResultErrorTryAsync(() => _databaseSet.AddAsync(entity).AsTask(), TableAccessError).
                  ToResultValueTaskAsync(entity.Id);

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

        /// <summary>
        /// Удалить все элементы в таблице
        /// </summary>
        public IResultError Remove() =>
            ResultErrorTry(() => _databaseSet.RemoveRange(_databaseSet), TableAccessError);
    }
}