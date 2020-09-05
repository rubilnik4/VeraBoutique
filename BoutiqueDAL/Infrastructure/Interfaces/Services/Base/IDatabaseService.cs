using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Interfaces.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Base
{
    /// <summary>
    /// Сервис получения данных из базы
    /// </summary>
    public interface IDatabaseService<TId, TModel>
        where TModel: IDomainModel<TId>
    {
        /// <summary>
        /// Получить модели из базы
        /// </summary>
        Task<IResultCollection<TModel>> Get();

        /// <summary>
        /// Получить модель из базы по идентификатору
        /// </summary>
        Task<IResultValue<TModel>> Get(TId id);

        /// <summary>
        /// Загрузить модели в базу
        /// </summary>
        Task<IResultCollection<TId>> Post(IEnumerable<TModel> models);

        /// <summary>
        /// Заменить модель в базе по идентификатору
        /// </summary>
        Task<IResultError> Put(TId id, TModel model);

        /// <summary>
        /// Удалить модель из базы по идентификатору
        /// </summary>
        Task<IResultError> Delete(TId id);
    }
}