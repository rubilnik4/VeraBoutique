using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных одежды
    /// </summary>
    public interface IClothesTable : IDatabaseTable<int, IClothesFullDomain, ClothesFullEntity>
    { }
}