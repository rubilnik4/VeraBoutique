﻿using System;
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
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Functional.Models.Interfaces.Result;

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
        /// Получить ошибку дублирования
        /// </summary>
        public async Task<IResultError> ValidateDuplicate(TId id) =>
             await _dataTable.FindExpressionAsync(ValidateIdQuery(id), id).
             ResultValueBindOkBadTaskAsync(
                okFunc: idValidate => GetDuplicateErrorResult(idValidate, _dataTable.GetType().Name),
                badFunc: _ => new ResultValue<TId>(Enumerable.Empty<IErrorResult>()));

        /// <summary>
        /// Получить ошибки дублирования
        /// </summary>
        public async Task<IResultError> ValidateDuplicates(IEnumerable<TId> ids) =>
             await _dataTable.FindsExpressionAsync(ValidateIdsQuery(ids)).
             ResultCollectionBindWhereTaskAsync(idsFind => idsFind.Count == 0,
                okFunc: idsFind => new ResultCollection<TId>(idsFind),
                badFunc: idsFind => GetDuplicateErrorsResult(idsFind, _dataTable.TableName));

        /// <summary>
        /// Проверить наличие сущности
        /// </summary>
        public async Task<IResultError> ValidateFind(TId id) =>
            await _dataTable.FindExpressionAsync(ValidateIdQuery(id), id);

        /// <summary>
        /// Проверить наличие сущностей
        /// </summary>
        public async Task<IResultError> ValidateFinds(IEnumerable<TId> ids) =>
           await _dataTable.FindsExpressionAsync(ValidateIdsQuery(ids));

        /// <summary>
        /// Проверить количество вложенных моделей
        /// </summary>
        public IResultError ValidateQuantity(IEnumerable<TId> ids) =>
           ids.
           ToResultCollectionWhere(
               idsValidate => idsValidate.Any(),
               _ => DatabaseErrors.ValueNotFoundError(typeof(TDomain).Name, _dataTable.TableName));

        /// <summary>
        /// Проверить вложенные моделей
        /// </summary>
        public async Task<IResultError> ValidateQuantityFind(IEnumerable<TId> ids) =>
            await new ResultError().
            ResultErrorBindOk(() => ValidateQuantity(ids)).
            ResultErrorBindOkAsync(() => ValidateFinds(ids));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        public virtual async Task<IResultError> ValidateIncludes(TDomain domain) =>
            await Task.FromResult(new ResultError());

        /// <summary>
        /// Проверить наличие коллекции вложенных моделей 
        /// </summary>
        public virtual async Task<IResultError> ValidateIncludes(IEnumerable<TDomain> domains) =>
            await Task.FromResult(new ResultError());

        /// <summary>
        /// Запрос проверки наличия дублирующей сущности
        /// </summary>
        private Func<IQueryable<TEntity>, IQueryable<TId>> ValidateIdQuery(TId id) =>
            entities => entities.Where(_dataTable.IdPredicate(id)).Select(_dataTable.IdSelect());

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