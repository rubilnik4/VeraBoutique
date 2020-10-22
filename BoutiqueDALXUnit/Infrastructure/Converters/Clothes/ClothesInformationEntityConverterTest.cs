using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesInformationEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesInformationDomains = ClothesData.ClothesInformationDomains.First();
            var colorClothesEntityConverter = new ColorClothesEntityConverter();
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);
            var clothesInformationEntityConverter = new ClothesInformationEntityConverter(colorClothesEntityConverter,
                                                                                          sizeGroupEntityConverter);

            var clothesInformationEntity = clothesInformationEntityConverter.ToEntity(clothesInformationDomains);
            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationEntity);

            Assert.True(clothesInformationDomains.Equals(clothesInformationAfterConverter));
        }
    }
}