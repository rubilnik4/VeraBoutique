using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable
{
    /// <summary>
    /// Таблица базы данных. Функции добавления, обновления и удаления данных
    /// </summary>
    public interface IDatabaseTableCrud<TId, TEntity>
        where TEntity : IEntityModel<TId>
        where TId : notnull
    {
        /// <summary>
        /// Добавить запись в таблицу
        /// </summary>
        Task<IResultValue<TEntity>> AddAsync(TEntity entity);

        /// <summary>
        /// Добавить список в таблицу
        /// </summary>
        Task<IResultCollection<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Добавить элемент в таблице
        /// </summary>
        IResultError Update(TEntity entity);

        /// <summary>
        /// Удалить элемент в таблице
        /// </summary>
        IResultValue<TEntity> Remove(TEntity entity);

        /// <summary>
        /// Удалить элементы в таблице
        /// </summary>
        IResultError RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удалить все элементы в таблице
        /// </summary>
        IResultError Remove();
    }
}