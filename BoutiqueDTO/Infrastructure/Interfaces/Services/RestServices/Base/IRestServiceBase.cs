using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис для данных Api
    /// </summary>
    public interface IRestServiceBase<in TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Получить данные
        /// </summary>
        IResultCollection<TDomain> Get();

        /// <summary>
        /// Получить данные по идентификатору
        /// </summary>
        IResultValue<TDomain> Get(TId id);

        /// <summary>
        /// Отправить данные
        /// </summary>
        IResultError Post(IEnumerable<TDomain> domains);

        /// <summary>
        /// Удалить все данные
        /// </summary>
        IResultError Delete();

        /// <summary>
        /// Получить данные асинхронно
        /// </summary>
        Task<IResultCollection<TDomain>> GetAsync();

        /// <summary>
        /// Получить данные по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TDomain>> GetAsync(TId id);

        /// <summary>
        /// Отправить данные асинхронно
        /// </summary>
        Task<IResultError> PostAsync(IEnumerable<TDomain> domains);

        /// <summary>
        /// Удалить все данные асинхронно
        /// </summary>
        Task<IResultError> DeleteAsync();
    }
}