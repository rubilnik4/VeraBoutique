using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes.ClothesEntities
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesShortDomains = ClothesData.ClothesDomains.First();
            var clothesShortEntityConverter = new ClothesEntityConverter();

            var colorClothesEntity = clothesShortEntityConverter.ToEntity(clothesShortDomains);
            var colorClothesAfterConverter = clothesShortEntityConverter.FromEntity(colorClothesEntity);

            Assert.True(colorClothesAfterConverter.OkStatus);
            Assert.True(clothesShortDomains.Equals(colorClothesAfterConverter.Value));
        }
    }
}