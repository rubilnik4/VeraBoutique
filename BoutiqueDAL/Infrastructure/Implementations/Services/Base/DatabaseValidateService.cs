using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Базовый сервис проверки данных из базы
    /// </summary>
    public abstract class DatabaseValidateService<TId, TDomain, TEntity> : IDatabaseValidateService<TId, TDomain>
       where TDomain : IDomainModel<TId>
       where TEntity : class, IEntityModel<TId>
       where TId : notnull
    {
        protected DatabaseValidateService(IDatabaseTable<TId, TDomain, TEntity> dataTable)
        {
            _dataTable = dataTable;
        }

        /// <summary>
        /// Таблица базы данных
        /// </summary>
        private readonly IDatabaseTable<TId, TDomain, TEntity> _dataTable;

        /// <summary>
        /// Проверить модель
        /// </summary>
        public abstract IResultError ValidateModel(TDomain domain);

        /// <summary>
        /// Проверить модели
        /// </summary>
        public IResultError ValidateModels(IEnumerable<TDomain> domains) =>
            domains.Select(ValidateModel).ToResultError();

        /// <summary>
        /// Комплексная проверка сущности для записи
        /// </summary>
        public async Task<IResultError> ValidatePost(TDomain domain) =>
            await new ResultError().
            ResultErrorBindOk(() => ValidateModel(domain)).
            ResultErrorBindOkAsync(() => ValidateDuplicate(domain.Id)).
            ResultErrorBindOkBindAsync(() => ValidateIncludes(domain));

        /// <summary>
        /// Комплексная проверка сущностей для записи
        /// </summary>
        public async Task<IResultError> ValidatePost(IEnumerable<TDomain> domains) =>
            await new ResultError().
            ResultErrorBindOk(() => ValidateModels(domains)).
            ResultErrorBindOkAsync(() => ValidateDuplicates(domains.Select(domain => domain.Id))).
            ResultErrorBindOkBindAsync(() => ValidateIncludes(domains));

        /// <summary>
        /// Комплексная проверка сущности для обновления
        /// </summary>
        public async Task<IResultError> ValidatePut(TDomain domain) =>
            await new ResultError().
            ResultErrorBindOk(() => ValidateModel(domain)).
            ResultErrorBindOkAsync(() => ValidateFind(domain.Id)).
            ResultErrorBindOkBindAsync(() => ValidateIncludes(domain));

        /// <summary>
        /// Проверить наличие сущности
        /// </summary>
        public async Task<IResultError> ValidateFind(TId id) =>
            await _dataTable.FindExpressionValueAsync(ValidateQuery(id), id).
            ResultValueBindWhereTaskAsync(idCount => idCount > 0,
                okFunc: _ => new ResultValue<TId>(id),
                badFunc: _ => new ResultValue<TId>(DatabaseErrors.ValueNotFoundError(id, _dataTable.TableName)));

        /// <summary>
        /// Проверить наличие сущностей
        /// </summary>
        public async Task<IResultError> ValidateFinds(IEnumerable<TId> ids) =>
           await ValidateFindsCollection(ids.ToList());

        /// <summary>
        /// Проверить наличие сущностей
        /// </summary>
        private async Task<IResultError> ValidateFindsCollection(IReadOnlyCollection<TId> ids) =>
           await _dataTable.FindsExpressionValueAsync(ValidateIdsQuery(ids)).
           ResultCollectionBindWhereTaskAsync(idsFind => ids.All(idsFind.Contains),
                okFunc: idsFind => new ResultCollection<TId>(idsFind),
                badFunc: idsFind => new ResultCollection<TId>(DatabaseErrors.ValuesNotFoundError(ids.Except(idsFind),
                                                                                                 _dataTable.TableName)));

        /// <summary>
        /// Проверить количество вложенных моделей
        /// </summary>
        public IResultError ValidateQuantity(IEnumerable<TDomain> domains) =>
           domains.ToResultCollectionWhere(idsValidate => idsValidate.Any(),
                                           collection => DatabaseErrors.CollectionEmpty(collection, _dataTable.TableName));

        /// <summary>
        /// Получить ошибку дублирования
        /// </summary>
        protected async Task<IResultError> ValidateDuplicate(TId id) =>
             await _dataTable.FindExpressionValueAsync(ValidateQuery(id), id).
             ResultValueBindWhereTaskAsync(idCount => idCount == 0,
                okFunc: _ => new ResultValue<TId>(id) ,
                badFunc: _ => GetDuplicateErrorResult(id, _dataTable.GetType().Name));

        /// <summary>
        /// Получить ошибки дублирования
        /// </summary>
        protected async Task<IResultError> ValidateDuplicates(IEnumerable<TId> ids) =>
             await _dataTable.FindsExpressionValueAsync(ValidateIdsQuery(ids)).
             ResultCollectionBindWhereTaskAsync(idsFind => idsFind.Count == 0,
                okFunc: idsFind => new ResultCollection<TId>(idsFind),
                badFunc: idsFind => GetDuplicateErrorsResult(idsFind, _dataTable.TableName));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected virtual async Task<IResultError> ValidateIncludes(TDomain domain) =>
            await Task.FromResult(new ResultError());

        /// <summary>
        /// Проверить наличие коллекции вложенных моделей 
        /// </summary>
        protected virtual async Task<IResultError> ValidateIncludes(IEnumerable<TDomain> domains) =>
            await Task.FromResult(new ResultError());

        /// <summary>
        /// Запрос проверки наличия дублирующей сущности
        /// </summary>
        private Func<IQueryable<TEntity>, Task<int>> ValidateQuery(TId id) =>
            entities => entities.Where(_dataTable.IdPredicate(id)).
                                 Select(_dataTable.IdSelect()).
                                 CountAsync();

        /// <summary>
        /// Запрос проверки наличия дублирующих сущностей
        /// </summary>
        private Func<IQueryable<TEntity>, IQueryable<TId>> ValidateIdsQuery(IEnumerable<TId> ids) =>
            entities => entities.Where(_dataTable.IdsPredicate(ids)).
                                 Select(_dataTable.IdSelect());

        /// <summary>
        /// Получить ошибку двойной записи
        /// </summary>
        private static IResultValue<TId> GetDuplicateErrorResult(TId id, string tableName) =>
            new ResultValue<TId>(DatabaseErrors.DuplicateError(id, tableName));

        /// <summary>
        /// Получить ошибки двойной записи
        /// </summary>
        private static IResultCollection<TId> GetDuplicateErrorsResult(IEnumerable<TId> ids, string tableName) =>
            new ResultCollection<TId>(DatabaseErrors.DuplicateErrors(ids, tableName));
    }
}