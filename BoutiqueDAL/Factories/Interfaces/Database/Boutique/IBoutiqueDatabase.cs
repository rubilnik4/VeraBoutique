using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Factories.Interfaces.Database.Boutique
{
    /// <summary>
    /// База данных магазина
    /// </summary>
    public interface IBoutiqueDatabase : IDatabase
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        IDatabaseTable<GenderType, IGenderEntity> GendersTable { get; }
    }
}