using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных
    /// </summary>
    public class ClothesShortEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        public static IClothesShortEntityConverter ClothesShortEntityConverter =>
            new ClothesShortEntityConverter();
    }
}