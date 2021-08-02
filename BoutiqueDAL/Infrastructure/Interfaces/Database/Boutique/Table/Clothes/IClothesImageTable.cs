using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных изображений одежды
    /// </summary>
    public interface IClothesImageTable : IDatabaseTable<int, IClothesImageDomain, ClothesImageEntity>
    { }
}