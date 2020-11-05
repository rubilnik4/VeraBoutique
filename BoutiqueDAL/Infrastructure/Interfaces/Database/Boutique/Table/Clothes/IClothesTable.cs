using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных одежды
    /// </summary>
    public interface IClothesTable : IDatabaseTable<int, ClothesEntity>
    { }
}