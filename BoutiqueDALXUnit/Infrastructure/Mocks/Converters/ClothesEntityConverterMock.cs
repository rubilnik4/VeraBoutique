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
            new ClothesEntityConverter(new ClothesShortEntityConverter(), GenderEntityConverterMock.GenderEntityConverter,
                                       ClothesTypeEntityConverterMock.ClothesTypeEntityConverter,
                                       ClothesTypeShortEntityConverterMock.ClothesTypeShortEntityConverter,
                                       ColorClothesEntityConverterMock.ColorClothesEntityConverter,
                                       SizeGroupEntityConverterMock.SizeGroupEntityConverter);
    }
}