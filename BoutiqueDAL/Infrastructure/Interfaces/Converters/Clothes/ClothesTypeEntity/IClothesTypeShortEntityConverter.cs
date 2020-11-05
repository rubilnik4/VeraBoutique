using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public interface IClothesTypeShortEntityConverter : 
        IEntityConverter<string, IClothesTypeShortDomain, IClothesTypeFullEntity, Models.Implementations.Entities.Clothes.ClothesTypeEntities.ClothesTypeFullEntity>
    { }
}