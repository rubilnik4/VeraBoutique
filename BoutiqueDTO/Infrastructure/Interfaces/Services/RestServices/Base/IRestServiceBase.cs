using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис загрузки в базу данных
    /// </summary>
    public interface IRestServiceBase<TId, in TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Загрузить данные
        /// </summary>
        Task<IResultError> Upload(IEnumerable<TDomain> domains);

        /// <summary>
        /// Удалить все данные
        /// </summary>
        Task<IResultError> Delete();
    }
}