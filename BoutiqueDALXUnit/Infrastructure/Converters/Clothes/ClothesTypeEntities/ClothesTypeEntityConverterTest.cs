using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
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
using Functional.Models.Interfaces.Errors.CommonErrors;
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
            var clothesTypeDomain = ClothesTypeData.ClothesTypeMainDomains.First();
            var clothesTypeEntityConverter = ClothesTypeMainEntityConverter;

            var clothesTypeEntity = clothesTypeEntityConverter.ToEntity(clothesTypeDomain);

            Assert.True(clothesTypeDomain.Equals(clothesTypeEntity));
            Assert.Null(clothesTypeEntity.Category);
        }

        /// <summary>
        /// Преобразования модели вида одежды из модели базы данных
        /// </summary>
        [Fact]
        public void FromEntity()
        {
            var clothesTypeEntity = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeEntityConverter = ClothesTypeMainEntityConverter;

            var clothesTypeDomain = clothesTypeEntityConverter.FromEntity(clothesTypeEntity);

            Assert.True(clothesTypeDomain.OkStatus);
            Assert.True(ClothesTypeData.ClothesTypeMainDomains.First().Equals(clothesTypeDomain.Value));
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных. Ошибка пола одежды
        /// </summary>
        [Fact]
        public void FromEntity_CategoryNotFound()
        {
            var clothesType = ClothesTypeEntitiesData.ClothesTypeEntities.First();
            var clothesTypeNull = GetClothesType(clothesType, clothesType.CategoryName, null, clothesType.Clothes);
            var clothesTypeEntityConverter = ClothesTypeMainEntityConverter;

            var clothesTypeAfterConverter = clothesTypeEntityConverter.FromEntity(clothesTypeNull);

            Assert.True(clothesTypeAfterConverter.HasErrors);
            Assert.IsType<IValueNotFoundErrorResult>(clothesTypeAfterConverter.Errors.First());
        }


        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private static IClothesTypeMainEntityConverter ClothesTypeMainEntityConverter =>
            ClothesTypeEntityConverterMock.ClothesTypeMainEntityConverter;

        /// <summary>
        /// Получить тип одежды
        /// </summary>
        private static ClothesTypeEntity GetClothesType(IClothesTypeBase clothesType,
                                                        string categoryName, CategoryEntity? category, 
                                                        IEnumerable<ClothesEntity>? clothes) =>
            new (clothesType.Name, clothesType.SizeTypeDefault, categoryName, category, clothes);
    }
}