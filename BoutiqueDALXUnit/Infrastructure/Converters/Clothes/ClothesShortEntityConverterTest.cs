using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesShortEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesShortDomains = ClothesData.ClothesShortDomains().First();
            var clothesShortEntityConverter = new ClothesShortEntityConverter();

            var colorClothesEntity = clothesShortEntityConverter.ToEntity(clothesShortDomains);
            var colorClothesAfterConverter = clothesShortEntityConverter.FromEntity(colorClothesEntity);

            Assert.True(clothesShortDomains.Equals(colorClothesAfterConverter));
        }
    }
}