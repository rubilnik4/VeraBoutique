using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ImageEntities
{
    /// <summary>
    /// Преобразования модели изображения в модель базы данных
    /// </summary>
    public interface IClothesImageEntityConverter : IEntityConverter<int, IClothesImageDomain, ClothesImageEntity>
    { }
}