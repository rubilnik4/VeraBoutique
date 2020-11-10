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
    public class ClothesInformationEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var clothesInformationDomain = ClothesData.ClothesInformationDomains.First();
            var clothesInformationEntityConverter = ClothesEntityConverter;

            var clothesInformationEntity = clothesInformationEntityConverter.ToEntity(clothesInformationDomain);
            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationEntity);

            Assert.True(clothesInformationAfterConverter.OkStatus);
            Assert.True(clothesInformationDomain.Equals(clothesInformationAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа пола
        /// </summary>
        [Fact]
        public void FromEntity_GenderNotFound()
        {
            var clothesInformation = ClothesEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesEntity(clothesInformation, clothesInformation.Description, 
                                                                            clothesInformation.GenderType, null,
                                                                            clothesInformation.ClothesTypeName, clothesInformation.ClothesTypeEntity,
                                                                            clothesInformation.ClothesColorComposites,
                                                                            clothesInformation.ClothesSizeGroupComposites);
            var clothesInformationEntityConverter = ClothesEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа одежды
        /// </summary>
        [Fact]
        public void FromEntity_ClothesTypeNotFound()
        {
            var clothesInformation = ClothesEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesEntity(clothesInformation, clothesInformation.Description,
                                                                            clothesInformation.GenderType, clothesInformation.Gender,
                                                                            clothesInformation.ClothesTypeName, null,
                                                                            clothesInformation.ClothesColorComposites,
                                                                            clothesInformation.ClothesSizeGroupComposites);
            var clothesInformationEntityConverter = ClothesEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка цвета одежды
        /// </summary>
        [Fact]
        public void FromEntity_ColorNotFound()
        {
            var clothesInformation = ClothesEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesEntity(clothesInformation, clothesInformation.Description,
                                                                            clothesInformation.GenderType, clothesInformation.Gender,
                                                                            clothesInformation.ClothesTypeName, clothesInformation.ClothesTypeEntity,
                                                                            null,
                                                                            clothesInformation.ClothesSizeGroupComposites);
            var clothesInformationEntityConverter = ClothesEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка группы размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizeGroupNotFound()
        {
            var clothesInformation = ClothesEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesEntity(clothesInformation, clothesInformation.Description,
                                                                            clothesInformation.GenderType, clothesInformation.Gender,
                                                                            clothesInformation.ClothesTypeName, clothesInformation.ClothesTypeEntity,
                                                                            clothesInformation.ClothesColorComposites,
                                                                            null);
            var clothesInformationEntityConverter = ClothesEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Конвертер модели цвета одежды в модель базы данных
        /// </summary>
        private static IClothesEntityConverter ClothesEntityConverter =>
            new ClothesEntityConverter(new ClothesShortEntityConverter(),
                                                  new GenderEntityConverter(),
                                                  new ClothesTypeEntityConverter(new CategoryEntityConverter(),
                                                                                 new GenderEntityConverter()),
                                                  new ColorClothesEntityConverter(),
                                                  new SizeGroupEntityConverter(new SizeEntityConverter()));
    }
}