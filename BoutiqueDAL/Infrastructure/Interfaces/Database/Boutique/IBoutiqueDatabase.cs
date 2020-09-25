using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
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
        IGenderTable GendersTable { get; }

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        IClothesTypeTable ClotheTypeTable { get; }
    }
}