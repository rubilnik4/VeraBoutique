using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public interface IClothesTypeEntityConverter : IEntityConverter<string, IClothesTypeFullDomain, IClothesTypeFullEntity, Models.Implementations.Entities.Clothes.ClothesTypeEntities.ClothesTypeFullEntity>
    { }
}