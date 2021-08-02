using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ImageEntities;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели изображения в модель базы данных
    /// </summary>
    public class ClothesImageEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели изображения в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesImageDomain = ClothesImageData.ClothesImageDomains.First();
            var clothesImageEntityConverter = new ClothesImageEntityConverter();

            var clothesImageEntity = clothesImageEntityConverter.ToEntity(clothesImageDomain);
            var clothesImageAfterConverter = clothesImageEntityConverter.FromEntity(clothesImageEntity);

            Assert.True(clothesImageAfterConverter.OkStatus);
            Assert.True(clothesImageDomain.Equals(clothesImageAfterConverter.Value));
        }
    }
}