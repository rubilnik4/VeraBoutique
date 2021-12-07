using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors.ResultErrorTryExtensions;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

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
        public async Task<IResultValue<TEntity>> AddAsync(TEntity entity) =>
            await ResultErrorTryAsync(() => _databaseSet.AddAsync(entity).AsTask(), TableAccessErrorType).
                  ToResultValueTaskAsync(entity);

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        public async Task<IResultCollection<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities) =>
            await ResultErrorTryAsync(() => _databaseSet.AddRangeAsync(entities), TableAccessErrorType).
                  ToResultValueTaskAsync(entities).
                  ToResultCollectionTaskAsync();

        /// <summary>
        /// Обновить элемент в таблице
        /// </summary>
        public new IResultError Update(TEntity entity) =>
            ResultErrorTry(() => _databaseSet.Update(entity), TableAccessErrorType);

        /// <summary>
        /// Удалить элемент в таблице
        /// </summary>
        public new IResultValue<TEntity> Remove(TEntity entity) =>
            ResultValueTry(() => _databaseSet.Remove(entity).
                                 Map(_ => entity), TableAccessErrorType);

        /// <summary>
        /// Удалить элементы в таблице
        /// </summary>
        public new IResultError RemoveRange(IEnumerable<TEntity> entities) =>
            ResultErrorTry(() => _databaseSet.RemoveRange(entities), TableAccessErrorType);

        /// <summary>
        /// Удалить все элементы в таблице
        /// </summary>
        public IResultError Remove() =>
            ResultErrorTry(() => _databaseSet.RemoveRange(EntitiesIncludesDelete), TableAccessErrorType);
    }
}