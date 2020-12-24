using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Connection;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Base
{
    /// <summary>
    /// Базовый сервис загрузки в базу данных
    /// </summary>
    public interface IUploadServiceBase<TId, in TDomain>
        where TDomain : IDomainModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Загрузить данные в базу
        /// </summary>
        Task<IResultError> Upload(IEnumerable<TDomain> domains);
    }
}