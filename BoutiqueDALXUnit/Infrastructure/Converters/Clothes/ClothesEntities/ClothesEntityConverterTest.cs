using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
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
    public class ClothesEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesDomain = ClothesData.ClothesDomains.First();
            var clothesEntityConverter = ClothesEntityConverter;

            var clothesEntity = clothesEntityConverter.ToEntity(clothesDomain);
            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesEntity);

            Assert.True(clothesAfterConverter.OkStatus);
            Assert.True(clothesDomain.Equals(clothesAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа пола
        /// </summary>
        [Fact]
        public void FromEntity_GenderNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.GenderType, clothes.ClothesTypeName,
                                               null, clothes.ClothesType,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverter;

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
            var clothesNull = GetClothesEntity(clothes, clothes.GenderType, clothes.ClothesTypeName,
                                               clothes.Gender, null,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverter;

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
            var clothesNull = GetClothesEntity(clothes, clothes.GenderType, clothes.ClothesTypeName,
                                               clothes.Gender, clothes.ClothesType,
                                               null, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverter;

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
            var clothesNull = GetClothesEntity(clothes, clothes.GenderType, clothes.ClothesTypeName,
                                               clothes.Gender, clothes.ClothesType,
                                               clothes.ClothesColorComposites, null);
            var clothesEntityConverter = ClothesEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели информации об одежде в модель базы данных
        /// </summary>
        private static IClothesEntityConverter ClothesEntityConverter =>
            ClothesEntityConverterMock.ClothesEntityConverter;

        /// <summary>
        /// Получить сущность одежды
        /// </summary>
        private static ClothesEntity GetClothesEntity(IClothesMain clothesMain,
                                                      GenderType genderType, string clothesTypeName,
                                                      GenderEntity? gender, ClothesTypeEntity? clothesType,
                                                      IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                                                      IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites) =>
            new ClothesEntity(clothesMain.Id, clothesMain.Name, clothesMain.Description, clothesMain.Price, clothesMain.Image,
                              genderType, clothesTypeName, gender, clothesType,
                              clothesColorComposites, clothesSizeGroupComposites);
    }
}