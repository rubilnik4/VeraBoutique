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
    public interface IDatabaseValidateService<TId, in TDomain>
       where TDomain : IDomainModel<TId>
       where TId : notnull
    {
        /// <summary>
        /// Получить ошибку дублирования
        /// </summary>
        Task<IResultError> ValidateDuplicate(TDomain domain);

        /// <summary>
        /// Получить ошибки дублирования
        /// </summary>
        Task<IResultError> ValidateDuplicates(IReadOnlyCollection<TDomain> domains);

        /// <summary>
        /// Проверить наличие модели
        /// </summary>
        Task<IResultError> ValidateValue(TDomain domain);

        /// <summary>
        /// Проверить наличие коллекции моделей 
        /// </summary>
        Task<IResultError> ValidateCollection(IReadOnlyCollection<TDomain> domains);
    }
}