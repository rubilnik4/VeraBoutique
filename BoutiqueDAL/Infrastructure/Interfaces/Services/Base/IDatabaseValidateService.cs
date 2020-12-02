using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
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
        /// Получить ошибку дублирования
        /// </summary>
        Task<IResultError> ValidateDuplicate(TId id);

        /// <summary>
        /// Получить ошибки дублирования
        /// </summary>
        Task<IResultError> ValidateDuplicates(IEnumerable<TId> ids);

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
        IResultError ValidateQuantity(IEnumerable<TId> ids);

        /// <summary>
        /// Проверить вложенные моделей
        /// </summary>
        Task<IResultError> ValidateQuantityFind(IEnumerable<TId> ids);

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        Task<IResultError> ValidateIncludes(TDomain domain);

        /// <summary>
        /// Проверить наличие коллекции вложенных моделей 
        /// </summary>
        Task<IResultError> ValidateIncludes(IEnumerable<TDomain> domains);
    }
}