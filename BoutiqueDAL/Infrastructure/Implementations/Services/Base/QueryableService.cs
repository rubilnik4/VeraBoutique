using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Base
{
    /// <summary>
    /// Сервис обработки запросов базы данных
    /// </summary>
    public class QueryableService<TId, TValue> : IQueryableService<TId, TValue>
        where TValue : class, IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Выгрузить данные из базы асинхронно
        /// </summary>
        public async Task<IReadOnlyCollection<TValue>> ToListAsync(IEnumerable<TValue> query) =>
           await query.AsQueryable().ToListAsync();

        /// <summary>
        /// Выгрузить первый элемент из базы асинхронно
        /// </summary>
        public async Task<TValue?> FirstOrDefaultAsync(IEnumerable<TValue> query) =>
            await query.AsQueryable().FirstOrDefaultAsync();
    }
}