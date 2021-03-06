﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.Models.Implementations.Result;
using Functional.Models.Implementations.ResultFactory;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Base
{
    /// <summary>
    /// Базовый сервис проверки данных из базы
    /// </summary>
    public interface IDatabaseValidateService<in TId, in TDomain>
       where TDomain : IDomainModel<TId>
       where TId : notnull
    {
        /// <summary>
        /// Комплексная проверка сущности для записи
        /// </summary>
        Task<IResultError> ValidatePost(TDomain domain);

        /// <summary>
        /// Комплексная проверка сущностей для записи
        /// </summary>
        Task<IResultError> ValidatePost(IEnumerable<TDomain> domains);

        /// <summary>
        /// Комплексная проверка сущности для обновления
        /// </summary>
        Task<IResultError> ValidatePut(TDomain domain);

        /// <summary>
        /// Проверить наличие сущности
        /// </summary>
        Task<IResultError> ValidateFind(TId id);

        /// <summary>
        /// Проверить наличие сущностей
        /// </summary>
        Task<IResultError> ValidateFinds(IEnumerable<TId> ids);

        /// <summary>
        /// Проверить количество вложенных моделей
        /// </summary>
        IResultError ValidateQuantity(IEnumerable<TDomain> domains);
    }
}