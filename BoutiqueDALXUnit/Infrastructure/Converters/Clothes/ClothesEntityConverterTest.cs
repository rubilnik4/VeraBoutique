using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDALXUnit.Data.Entities;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
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
            var clothesNull = new ClothesEntity(clothes, clothes.GenderType, null,
                                                clothes.ClothesTypeName, clothes.ClothesTypeShort,
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
            var clothesNull = new ClothesEntity(clothes, clothes.GenderType, clothes.Gender,
                                                clothes.ClothesTypeName, null,
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
            var clothesNull = new ClothesEntity(clothes,
                                                clothes.GenderType, clothes.Gender,
                                                clothes.ClothesTypeName, clothes.ClothesTypeShort,
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
            var clothesNull = new ClothesEntity(clothes, clothes.GenderType, clothes.Gender,
                                                clothes.ClothesTypeName, clothes.ClothesTypeShort,
                                                clothes.ClothesColorComposites, null);
            var clothesEntityConverter = ClothesEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Конвертер модели цвета одежды в модель базы данных
        /// </summary>
        private static IClothesEntityConverter ClothesEntityConverter =>
            new ClothesEntityConverter(new ClothesShortEntityConverter(), new GenderEntityConverter(),
                                       new ClothesTypeShortEntityConverter(new CategoryEntityConverter()),
                                       new ColorClothesEntityConverter(),
                                       new SizeGroupEntityConverter(new SizeEntityConverter()));
    }
}