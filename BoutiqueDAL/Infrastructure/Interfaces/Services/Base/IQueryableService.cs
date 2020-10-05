using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Base
{
    /// <summary>
    /// Сервис обработки запросов базы данных
    /// </summary>
    public interface IQueryableService<TId, TValue> 
        where TValue: class, IEntityModel<TId>
        where TId: notnull
    {
        /// <summary>
        /// Выгрузить данные из базы асинхронно
        /// </summary>
        Task<IReadOnlyCollection<TValue>> ToListAsync(IEnumerable<TValue> query);
    }
}