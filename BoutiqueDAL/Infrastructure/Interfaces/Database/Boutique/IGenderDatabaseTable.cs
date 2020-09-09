using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public interface IGenderDatabaseTable<TId, TEntity> : IDatabaseTable<TId, TEntity>
        where TEntity : class, IEntityModel<TId>
        where TId : notnull
    { }
}