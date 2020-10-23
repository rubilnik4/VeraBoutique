using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDALXUnit.Data;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesTypeEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesTypeDomain = ClothesTypeData.GetClothesTypeDomain().First(); 
            var clothesTypeEntityConverter = new ClothesTypeEntityConverter(new CategoryEntityConverter());

            var clothesTypeEntity = clothesTypeEntityConverter.ToEntity(clothesTypeDomain);
            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeDomain.Equals(clothesTypeAfterConverter));
        }
    }
}