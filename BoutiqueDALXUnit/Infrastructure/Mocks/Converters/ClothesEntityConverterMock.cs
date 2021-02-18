using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Конвертер модели цвета одежды в модель базы данных
    /// </summary>
    public static class ClothesEntityConverterMock
    {
        /// <summary>
        /// Конвертер модели цвета одежды в модель базы данных
        /// </summary>d
        public static IClothesEntityConverter ClothesEntityConverter =>
            new ClothesEntityConverter(GenderEntityConverterMock.GenderEntityConverter,
                                       ClothesTypeEntityConverterMock.ClothesTypeEntityConverter,
                                       ColorClothesEntityConverterMock.ColorClothesEntityConverter,
                                       SizeGroupEntityConverterMock.SizeGroupEntityConverter);

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        public static IClothesShortEntityConverter ClothesShortEntityConverter =>
            new ClothesShortEntityConverter();
    }
}