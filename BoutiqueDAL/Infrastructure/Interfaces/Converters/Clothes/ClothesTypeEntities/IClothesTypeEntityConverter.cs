using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public interface IClothesTypeEntityConverter : IEntityConverter<string, IClothesTypeDomain, ClothesTypeEntity>
    { }
}