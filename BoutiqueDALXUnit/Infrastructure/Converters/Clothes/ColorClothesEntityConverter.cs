using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных. Тесты
    /// </summary>
    public class ColorClothesEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var colorClothesDomain = ColorClothesData.GetColorClothesDomain().First();
            var colorClothesEntityConverter = new ColorClothesEntityConverter();

            var colorClothesEntity = colorClothesEntityConverter.ToEntity(colorClothesDomain);
            var colorClothesAfterConverter = colorClothesEntityConverter.FromEntity(colorClothesEntity);

            Assert.True(colorClothesAfterConverter.OkStatus);
            Assert.True(colorClothesDomain.Equals(colorClothesAfterConverter.Value));
        }
    }
}