using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Functional.Models.Enums;
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
            var clothesTypeDomain = ClothesTypeData.ClothesTypeDomain.First();
            var clothesTypeShortEntityConverter = ClothesTypeEntityConverterMock.ClothesTypeShortEntityConverter;

            var clothesTypeEntity = clothesTypeShortEntityConverter.ToEntity(clothesTypeDomain);
            var clothesTypeAfterConverter = clothesTypeShortEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeAfterConverter.OkStatus);
            Assert.True(clothesTypeDomain.Equals(clothesTypeAfterConverter.Value));
        }
    }
}