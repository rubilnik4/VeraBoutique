using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Base
{
    /// <summary>
    /// Сервис получения данных из базы
    /// </summary>
    public interface IDatabaseService<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Получить полные модели из базы
        /// </summary>
        Task<IResultCollection<TDomain>> Get();

        /// <summary>
        /// Получить  полную модель из базы по идентификатору
        /// </summary>
        Task<IResultValue<TDomain>> Get(TId id);

        /// <summary>
        /// Загрузить модель в базу
        /// </summary>
        Task<IResultValue<TId>> Post(TDomain model);

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        Task<IResultCollection<TId>> Post(IEnumerable<TDomain> models);

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        Task<IResultError> Put(TDomain model);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        Task<IResultValue<TDomain>> Delete(TId id);

        /// <summary>
        /// Проверить наличие моделей
        /// </summary>
        Task<IResultError> Validate(TDomain model);

        /// <summary>
        /// Проверить наличие моделей
        /// </summary>
        Task<IResultError> Validate(IEnumerable<TDomain> models);
    }
}