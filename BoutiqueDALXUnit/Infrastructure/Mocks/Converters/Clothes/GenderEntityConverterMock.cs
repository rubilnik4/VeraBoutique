using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели типа пола и модель базы данных
    /// </summary>
    public class GenderEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        public static IGenderEntityConverter GenderEntityConverter =>
             new GenderEntityConverter();

        /// <summary>
        /// Преобразования модели типа пола с категорией и модель базы данных
        /// </summary>
        public static IGenderCategoryEntityConverter GenderCategoryEntityConverter =>
             new GenderCategoryEntityConverter(CategoryEntityConverterMock.CategoryClothesTypeEntityConverter);
    }
}