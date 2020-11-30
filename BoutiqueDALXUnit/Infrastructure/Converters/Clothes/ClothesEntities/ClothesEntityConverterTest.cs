using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
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
            var clothesNull = new ClothesEntity(clothes, null, clothes.ClothesType,
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
            var clothesNull = new ClothesEntity(clothes, clothes.Gender, null,
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
            var clothesNull = new ClothesEntity(clothes, clothes.Gender, clothes.ClothesType,
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
            var clothesNull = new ClothesEntity(clothes, clothes.Gender, clothes.ClothesType,
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
    }
}