using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public interface IGenderDatabaseTable: IDatabaseTable<GenderType, GenderEntity>
    { }
}