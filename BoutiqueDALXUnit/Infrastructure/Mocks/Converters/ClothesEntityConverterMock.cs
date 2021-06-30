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
        /// Конвертер модели одежды в модель базы данных
        /// </summary>d
        public static IClothesMainEntityConverter ClothesMainEntityConverter =>
            new ClothesMainEntityConverter(GenderEntityConverterMock.GenderEntityConverter,
                                       ClothesTypeEntityConverterMock.ClothesTypeEntityConverter,
                                       ColorClothesEntityConverterMock.ColorClothesEntityConverter,
                                       SizeGroupEntityConverterMock.SizeGroupMainEntityConverter);

        /// <summary>
        /// Конвертер модели одежды в модель базы данных
        /// </summary>d
        public static IClothesDetailEntityConverter ClothesDetailEntityConverter =>
            new ClothesDetailEntityConverter(ColorClothesEntityConverterMock.ColorClothesEntityConverter,
                                             SizeGroupEntityConverterMock.SizeGroupMainEntityConverter);

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        public static IClothesEntityConverter ClothesEntityConverter =>
            new ClothesEntityConverter();
    }
}