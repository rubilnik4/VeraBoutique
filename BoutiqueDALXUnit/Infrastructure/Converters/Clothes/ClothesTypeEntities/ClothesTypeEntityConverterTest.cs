using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Functional.Models.Enums;
using Xunit;
using Xunit.Sdk;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes.ClothesTypeEntities
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
        public void ToEntity()
        {
            var clothesTypeDomain = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;

            var clothesTypeEntity = clothesTypeEntityConverter.ToEntity(clothesTypeDomain);

            Assert.True(clothesTypeDomain.Equals(clothesTypeEntity));
            Assert.Null(clothesTypeEntity.Category);
            Assert.True(clothesTypeEntity.ClothesTypeGenderComposites?.All(composite => composite.Gender == null));
        }

        /// <summary>
        /// Преобразования модели вида одежды из модели базы данных
        /// </summary>
        [Fact]
        public void FromEntity()
        {
            var clothesTypeEntity = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;

            var clothesTypeDomain = clothesTypeEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeDomain.OkStatus);
            Assert.True(ClothesTypeData.ClothesTypeDomains.First().Equals(clothesTypeDomain.Value));
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных. Ошибка пола одежды
        /// </summary>
        [Fact]
        public void FromEntity_CategoryNotFound()
        {
            var clothesType = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeNull = GetClothesType(clothesType, clothesType.CategoryName, null,
                                                 clothesType.ClothesTypeGenderComposites, clothesType.Clothes);
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;

            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeNull);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных. Ошибка пола одежды
        /// </summary>
        [Fact]
        public void FromEntity_GendersNotFound()
        {
            var clothesType = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeNull = GetClothesType(clothesType, clothesType.CategoryName, clothesType.Category, 
                                                 null, clothesType.Clothes);
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;

            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeNull);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.True(clothesTypeAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private static IClothesTypeEntityConverter ClothesTypeEntityConverter =>
            ClothesTypeEntityConverterMock.ClothesTypeEntityConverter;

        /// <summary>
        /// Получить тип одежды
        /// </summary>
        private static ClothesTypeEntity GetClothesType(IClothesTypeBase clothesType,
                                                        string categoryName, CategoryEntity? category, 
                                                        IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites, 
                                                        IEnumerable<ClothesFullEntity>? clothes) =>
            new (clothesType.Name, categoryName, category, clothesTypeGenderComposites, clothes);
    }
}