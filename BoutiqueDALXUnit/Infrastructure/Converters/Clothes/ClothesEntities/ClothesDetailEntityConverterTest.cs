using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
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
    /// Преобразования модели уточненной информации об одежде в модель базы данных. Тесты
    /// </summary>
    public class ClothesDetailEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity()
        {
            var clothesDomain = ClothesData.ClothesDetailDomains.First();
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesDetailEntityConverter;

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
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesDetailEntityConverter;

            var clothesDomain = clothesEntityConverter.FromEntity(clothesEntity);

            Assert.True(clothesDomain.OkStatus);
            Assert.True(ClothesData.ClothesDetailDomains.First().Equals(clothesDomain.Value));
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка цвета одежды
        /// </summary>
        [Fact]
        public void FromEntity_ColorNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.Image, clothes.Gender, clothes.ClothesType,
                                               null, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesDetailEntityConverter;

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
            var clothesNull = GetClothesEntity(clothes, clothes.Image, clothes.Gender, clothes.ClothesType,
                                               clothes.ClothesColorComposites, null);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesDetailEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.True(clothesAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Получить сущность одежды
        /// </summary>
        private static ClothesEntity GetClothesEntity(IClothesBase clothes, byte[] image,
                                                      GenderEntity? gender, ClothesTypeEntity? clothesType,
                                                      IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                                                      IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites) =>
            new(clothes.Id, clothes.Name, clothes.Description, clothes.Price, image,
                 clothes.GenderType, clothes.ClothesTypeName, gender, clothesType,
                 clothesColorComposites, clothesSizeGroupComposites);
    }
}