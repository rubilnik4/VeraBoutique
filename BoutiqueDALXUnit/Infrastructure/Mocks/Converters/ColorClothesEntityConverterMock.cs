using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Converters
{
    /// <summary>
    /// Преобразования модели цвета одежды в модель базы данных
    /// </summary>
    public class ColorClothesEntityConverterMock
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        public static IColorClothesEntityConverter ColorClothesEntityConverter =>
            new ColorClothesEntityConverter();
    }
}