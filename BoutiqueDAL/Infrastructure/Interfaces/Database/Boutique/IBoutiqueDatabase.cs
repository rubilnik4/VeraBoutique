using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique
{
    /// <summary>
    /// База данных магазина
    /// </summary>
    public interface IBoutiqueDatabase : IDatabase
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        IDatabaseTable<GenderType, GenderEntity> GendersTable { get; }
    }
}