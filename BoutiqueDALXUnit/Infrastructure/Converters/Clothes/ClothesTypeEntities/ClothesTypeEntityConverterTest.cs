using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data.Entities;
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
        public void ToEntity_FromEntity()
        {
            var clothesTypeDomain = ClothesTypeData.ClothesTypeDomains.First();
            var clothesTypeEntityConverter = ClothesTypeEntityConverter;

            var clothesTypeEntity = clothesTypeEntityConverter.ToEntity(clothesTypeDomain);
            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeAfterConverter.OkStatus);
            Assert.True(clothesTypeDomain.Equals(clothesTypeAfterConverter.Value));
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
            new ClothesTypeEntityConverter(new CategoryEntityConverter(), new GenderEntityConverter());

        /// <summary>
        /// Получить тип одежды
        /// </summary>
        private static ClothesTypeEntity GetClothesType(IClothesType clothesType,
                                                        string categoryName, CategoryEntity? category, 
                                                        IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites, 
                                                        IEnumerable<ClothesEntity>? clothes) =>
            new ClothesTypeEntity(clothesType.Name, categoryName, category, clothesTypeGenderComposites, clothes);
    }
}