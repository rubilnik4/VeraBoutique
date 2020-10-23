using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных
    /// </summary>
    public interface IClothesShortEntityConverter : IEntityConverter<int, IClothesShortDomain, IClothesShortEntity, ClothesShortEntity>
    { }
}