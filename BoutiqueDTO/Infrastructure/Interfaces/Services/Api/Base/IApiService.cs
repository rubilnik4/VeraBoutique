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
        /// Получить данные Api
        /// </summary>
        IResultCollection<TTransfer> Get();

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        IResultValue<TTransfer> Get(TId id);

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        IResultValue<TId> Post(TTransfer transfer);

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        IResultCollection<TId> PostCollection(IEnumerable<TTransfer> transfers);

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        IResultError Put(TTransfer transfer);

        /// <summary>
        /// Удалить все данные Api
        /// </summary>
        IResultError Delete();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        IResultValue<TTransfer> Delete(TId id);

        /// <summary>
        /// Получение данных
        /// </summary>
        Task<IResultCollection<TTransfer>> GetAsync();

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        Task<IResultValue<TTransfer>> GetAsync(TId id);

        /// <summary>
        /// Добавление данных
        /// </summary>
        Task<IResultValue<TId>> PostAsync(TTransfer transfer);

        /// <summary>
        /// Добавление коллекции данных
        /// </summary>
        Task<IResultCollection<TId>> PostCollectionAsync(IEnumerable<TTransfer> transfers);

        /// <summary>
        /// Добавление данных
        /// </summary>
        Task<IResultError> PutAsync(TTransfer transfer);

        /// <summary>
        /// Удалить все данные Api
        /// </summary>
        Task<IResultError> DeleteAsync();

        /// <summary>
        /// Удаление данных
        /// </summary>
        Task <IResultValue<TTransfer>> DeleteAsync(TId id);
    }
}