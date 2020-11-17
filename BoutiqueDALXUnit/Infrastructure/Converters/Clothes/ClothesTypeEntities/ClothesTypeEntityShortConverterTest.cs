using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDALXUnit.Data.Entities;
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
            var clothesTypeShortEntityConverter = ClothesTypeShortEntityConverter;

            var clothesTypeEntity = clothesTypeShortEntityConverter.ToEntity(clothesTypeDomain);
            var clothesTypeAfterConverter = clothesTypeShortEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeAfterConverter.OkStatus);
            Assert.True(clothesTypeDomain.Equals(clothesTypeAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных. Ошибка категории одежды
        /// </summary>
        [Fact]
        public void FromEntity_CategoryNotFound()
        {
            var clothesType = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeNull = new ClothesTypeShortEntity(clothesType.Name, clothesType.CategoryName, null,
                                                             clothesType.Clothes);
            var clothesTypeEntityConverter = ClothesTypeShortEntityConverter;

            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeNull);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private static IClothesTypeShortEntityConverter ClothesTypeShortEntityConverter =>
            new ClothesTypeShortEntityConverter(new CategoryEntityConverter());
    }
}