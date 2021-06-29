using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис для данных Api
    /// </summary>
    public interface IRestServiceBase<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Получить данные асинхронно
        /// </summary>
        Task<IResultCollection<TDomain>> GetAsync();

        /// <summary>
        /// Получить данные по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TDomain>> GetAsync(TId id);

        /// <summary>
        /// Отправить данные коллекции асинхронно
        /// </summary>
        Task<IResultCollection<TId>> PostCollectionAsync(IEnumerable<TDomain> domains);

        /// <summary>
        /// Отправить данные асинхронно
        /// </summary>
        Task<IResultValue<TId>> PostValueAsync(TDomain domain);

        /// <summary>
        /// Обновить данные асинхронно
        /// </summary>
        Task<IResultError> PutAsync(TDomain domain);

        /// <summary>
        /// Удалить все данные асинхронно
        /// </summary>
        Task<IResultError> DeleteAsync();

        /// <summary>
        /// Удалить данные по идентификатору асинхронно
        /// </summary>
        Task<IResultValue<TDomain>> DeleteAsync(TId id);
    }
}