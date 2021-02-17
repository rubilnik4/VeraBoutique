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
        /// Отправить данные
        /// </summary>
        IResultCollection<TDomain> Get();

        /// <summary>
        /// Отправить данные
        /// </summary>
        IResultError Post(IEnumerable<TDomain> domains);

        /// <summary>
        /// Удалить все данные
        /// </summary>
        IResultError Delete();

        /// <summary>
        /// Получить данные
        /// </summary>
        Task<IResultCollection<TDomain>> GetAsync();

        /// <summary>
        /// Отправить данные
        /// </summary>
        Task<IResultError> PostAsync(IEnumerable<TDomain> domains);

        /// <summary>
        /// Удалить все данные
        /// </summary>
        Task<IResultError> DeleteAsync();
    }
}