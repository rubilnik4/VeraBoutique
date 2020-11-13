using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Базовый сервис проверки данных из базы
    /// </summary>
    public class DatabaseValidateService<TId, TDomain, TEntity>: IDatabaseValidateService<TId, TDomain>
       where TDomain : IDomainModel<TId>
       where TEntity : class, IEntityModel<TId>
       where TId : notnull
    {
        public DatabaseValidateService(IDatabaseTable<TId, TDomain, TEntity> dataTable)
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
        public async Task<IResultError> ValidateDuplicate(TDomain domain) =>
             await _dataTable.FindExpressionAsync(ValidateDuplicateQuery(domain)).
             ResultValueOkBindAsync(id => GetDuplicateErrorResult(id, _dataTable.GetType().Name));

        /// <summary>
        /// Получить ошибки дублирования
        /// </summary>
        public async Task<IResultError> ValidateDuplicates(IReadOnlyCollection<TDomain> domains) =>
             await _dataTable.FindsExpressionAsync(ValidateDuplicatesQuery(domains)).
             ResultCollectionBindWhereBindAsync(ids => ids.Count == 0,
                okFunc: ResultCollectionFactory.CreateTaskResultCollectionAsync,
                badFunc: ids => GetDuplicateErrorsResult(ids, _dataTable.TableName));

        /// <summary>
        /// Проверить наличие модели
        /// </summary>
        public async Task<IResultError> ValidateValue(TDomain domain) =>
            await _dataTable.FindExpressionAsync(ValidateQuery(domain));

        /// <summary>
        /// Проверить наличие коллекции моделей 
        /// </summary>
        public async Task<IResultError> ValidateCollection(IReadOnlyCollection<TDomain> domains) =>
            await _dataTable.FindsExpressionAsync(ValidateQuery(domains)).
            ResultCollectionOkTaskAsync(ids => domains.Where(model => !ids.Contains(model.Id))).
            ResultCollectionBindErrorsOkTaskAsync(entitiesNotFound =>
                entitiesNotFound.
                Select(entityNotFound => DatabaseErrors.ValueNotFoundError(entityNotFound.Id?.ToString() ?? String.Empty,
                                                                           _dataTable.GetType().Name)).
                Map(errors => new ResultError(errors)));

        /// <summary>
        /// Запрос проверки наличия дублирующей сущности
        /// </summary>
        private Func<IQueryable<TEntity>, IQueryable<TId>> ValidateDuplicateQuery(TDomain domain) =>
            entities => entities.Where(_dataTable.IdPredicate(domain.Id)).Select(_dataTable.IdSelect());

        /// <summary>
        /// Запрос проверки наличия дублирующих сущностей
        /// </summary>
        private Func<IQueryable<TEntity>, IQueryable<TId>> ValidateDuplicatesQuery(IReadOnlyCollection<TDomain> domains) =>
            entities => entities.Where(_dataTable.IdsPredicate(domains.Select(domain => domain.Id))).
                                 Select(_dataTable.IdSelect());

        /// <summary>
        /// Запрос проверки наличия сущностей
        /// </summary>
        private Func<IQueryable<TEntity>, IQueryable<TId>> ValidateQuery(TDomain domain) =>
            entities => _dataTable.ValidateFilter(entities, domain).Select(_dataTable.IdSelect());

        /// <summary>
        /// Запрос проверки наличия сущностей
        /// </summary>
        private Func<IQueryable<TEntity>, IQueryable<TId>> ValidateQuery(IReadOnlyCollection<TDomain> domains) =>
            entities => _dataTable.ValidateFilter(entities, domains).Select(_dataTable.IdSelect());

        /// <summary>
        /// Получить ошибку двойной записи
        /// </summary>
        private static async Task<IResultValue<TId>> GetDuplicateErrorResult(TId id, string tableName) =>
            await ResultValueFactory.
            CreateTaskResultValueError<TId>(DatabaseErrors.DuplicateError(id, tableName));

        /// <summary>
        /// Получить ошибки двойной записи
        /// </summary>
        private static async Task<IResultCollection<TId>> GetDuplicateErrorsResult(IEnumerable<TId> ids, string tableName) =>
            await ResultCollectionFactory.
            CreateTaskResultCollectionError<TId>(DatabaseErrors.DuplicateErrors(ids, tableName));
    }
}