using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class CategoryEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели основных данных вида одежды в модель базы данных
        /// </summary>
        public static ICategoryEntityConverter CategoryEntityConverter =>
            new CategoryEntityConverter();

        /// <summary>
        /// Преобразования модели категории одежды с типом в модель базы данных
        /// </summary>
        public static ICategoryClothesTypeEntityConverter CategoryClothesTypeEntityConverter =>
            new CategoryClothesTypeEntityConverter(ClothesTypeEntityConverterMock.ClothesTypeEntityConverter);
    }
}