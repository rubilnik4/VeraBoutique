using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using ResultFunctional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesTypeEntityShortConverterTest
    {
        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesTypeShortDomain = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeShortEntityConverter = new ClothesTypeEntityConverter();

            var clothesTypeShortEntity = clothesTypeShortEntityConverter.ToEntity(clothesTypeShortDomain);
            var clothesTypeAfterConverter = clothesTypeShortEntityConverter.FromEntity(clothesTypeShortEntity);

            Assert.True(clothesTypeAfterConverter.OkStatus);
            Assert.True(clothesTypeShortEntity.Equals(clothesTypeAfterConverter.Value));
        }
    }
}