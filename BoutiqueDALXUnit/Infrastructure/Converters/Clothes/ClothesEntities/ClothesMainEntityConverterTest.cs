using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes.ClothesEntities
{
    /// <summary>
    /// Преобразования модели одежды в модель базы данных. Тесты
    /// </summary>
    public class ClothesMainEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity()
        {
            var clothesDomain = ClothesData.ClothesMainDomains.First();
            var clothesEntityConverter = ClothesMainEntityConverter;

            var clothesEntity = clothesEntityConverter.ToEntity(clothesDomain);

            Assert.True(clothesDomain.Equals(clothesEntity));
            Assert.Null(clothesEntity.Gender);
            Assert.Null(clothesEntity.ClothesType);
            Assert.True(clothesEntity.ClothesSizeGroupComposites?.All(composite => composite.SizeGroup == null));
            Assert.True(clothesEntity.ClothesColorComposites?.All(composite => composite.ColorClothes == null));
        }

        /// <summary>
        /// Преобразования модели цвета одежды из модели базы данных
        /// </summary>
        [Fact]
        public void FromEntity()
        {
            var clothesEntity = ClothesEntitiesData.ClothesEntities.First();
            var clothesEntityConverter = ClothesMainEntityConverter;

            var clothesDomain = clothesEntityConverter.FromEntity(clothesEntity);

            Assert.True(clothesDomain.OkStatus);
            Assert.True(ClothesData.ClothesDomains.First().Equals(clothesDomain.Value));
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа пола
        /// </summary>
        [Fact]
        public void FromEntity_GenderNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, null, clothes.ClothesType,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа одежды
        /// </summary>
        [Fact]
        public void FromEntity_ClothesTypeNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.Gender, null,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка цвета одежды
        /// </summary>
        [Fact]
        public void FromEntity_ColorNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.Gender, clothes.ClothesType,
                                               null, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка группы размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizeGroupNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.Gender, clothes.ClothesType,
                                               clothes.ClothesColorComposites, null);
            var clothesEntityConverter = ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в модель базы данных
        /// </summary>
        private static IClothesMainEntityConverter ClothesMainEntityConverter =>
            ClothesEntityConverterMock.ClothesMainEntityConverter;

        /// <summary>
        /// Получить сущность одежды
        /// </summary>
        private static ClothesEntity GetClothesEntity(IClothesBase clothes,
                                                      GenderEntity? gender, ClothesTypeEntity? clothesType,
                                                      IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                                                      IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites) =>
            new(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                 clothes.GenderType, clothes.ClothesTypeName, gender, clothesType,
                 clothesColorComposites, clothesSizeGroupComposites);
    }
}