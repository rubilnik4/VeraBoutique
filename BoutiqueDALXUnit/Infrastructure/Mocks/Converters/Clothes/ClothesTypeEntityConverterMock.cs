using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        public static IClothesTypeMainEntityConverter ClothesTypeMainEntityConverter =>
            new ClothesTypeMainEntityConverter(CategoryEntityConverterMock.CategoryEntityConverter);

        /// <summary>
        /// Преобразования модели основных данных вида одежды в модель базы данных
        /// </summary>
        public static IClothesTypeEntityConverter ClothesTypeEntityConverter =>
            new ClothesTypeEntityConverter();
    }
}