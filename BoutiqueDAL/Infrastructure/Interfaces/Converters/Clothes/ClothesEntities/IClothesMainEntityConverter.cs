using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities
{
    /// <summary>
    /// Преобразования модели об одежде в модель базы данных
    /// </summary>
    public interface IClothesMainEntityConverter : 
        IEntityConverter<int, IClothesMainDomain, ClothesEntity>
    { }
}