using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeShortEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели основных данных вида одежды в модель базы данных
        /// </summary>
        public static IClothesTypeShortEntityConverter ClothesTypeShortEntityConverter =>
            new ClothesTypeShortEntityConverter(CategoryEntityConverterMock.CategoryEntityConverter);
    }
}