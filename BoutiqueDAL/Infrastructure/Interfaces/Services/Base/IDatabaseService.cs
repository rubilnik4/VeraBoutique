using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using ResultFunctional.Models.Interfaces.Results;

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
        Task<IResultValue<TDomain>> Post(TDomain cart);

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        Task<IResultCollection<TDomain>> Post(IEnumerable<TDomain> models);

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        Task<IResultError> Put(TDomain model);

        /// <summary>
        /// Удалить все модели из базы
        /// </summary>
        Task<IResultError> Delete();

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        Task<IResultValue<TId>> Delete(TId id);
    }
}