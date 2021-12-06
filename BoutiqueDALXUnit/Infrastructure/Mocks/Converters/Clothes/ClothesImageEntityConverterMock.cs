using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ImageEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ImageEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели изображения в модель базы данных
    /// </summary>
    public class ClothesImageEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели изображения в модель базы данных
        /// </summary>
        public static IClothesImageEntityConverter ClothesImageEntityConverter =>
            new ClothesImageEntityConverter();
    }
}