using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base
{
    /// <summary>
    /// Базовый сервис получения данных по протоколу rest api
    /// </summary>
    public interface IApiService<TId, TTransfer>
        where TTransfer: ITransferModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        Task<IResultCollection<TTransfer>> Get();

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        Task<IResultValue<TTransfer>> Get(TId id);

        /// <summary>
        /// Добавление данных
        /// </summary>
        Task<IResultValue<TId>> Post(TTransfer transfer);

        /// <summary>
        /// Добавление коллекции данных
        /// </summary>
        Task<IResultCollection<TId>> PostCollection(IEnumerable<TTransfer> transfers);

        /// <summary>
        /// Добавление данных
        /// </summary>
        Task<IResultError> Put(TTransfer transfer);

        /// <summary>
        /// Удаление данных
        /// </summary>
        Task<IResultValue<TTransfer>> Delete(TId id);
    }
}