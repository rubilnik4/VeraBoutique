using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using Functional.Models.Enums;
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
            var clothesTypeDomain = ClothesTypeData.GetClothesTypeFullDomain().First(); 
            var clothesTypeEntityConverter = new ClothesTypeEntityConverter(new CategoryEntityConverter());

            var clothesTypeEntity = clothesTypeEntityConverter.ToEntity(clothesTypeDomain);
            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeAfterConverter.OkStatus);
            Assert.True(clothesTypeDomain.Equals(clothesTypeAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных. Ошибка категории одежды
        /// </summary>
        [Fact]
        public void FromEntity_CategoryNotFound()
        {
            var clothesType= ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeNull = new ClothesTypeFullEntity(clothesType.Name, clothesType.CategoryName, null,
                                                        clothesType.Clothes,
                                                        clothesType.ClothesTypeGenderComposites);
            var clothesTypeEntityConverter = new ClothesTypeEntityConverter(new CategoryEntityConverter());

            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeNull);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}