using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
            var clothesInformationEntityConverter = ClothesInformationEntityConverter;

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
            var clothesInformation = ClothesInformationEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesInformationEntity(clothesInformation, clothesInformation.Description, 
                                                                            clothesInformation.GenderType, null,
                                                                            clothesInformation.ClothesTypeName, clothesInformation.ClothesTypeEntity,
                                                                            clothesInformation.ClothesColorCompositeEntities,
                                                                            clothesInformation.ClothesSizeGroupCompositeEntities);
            var clothesInformationEntityConverter = ClothesInformationEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа одежды
        /// </summary>
        [Fact]
        public void FromEntity_ClothesTypeNotFound()
        {
            var clothesInformation = ClothesInformationEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesInformationEntity(clothesInformation, clothesInformation.Description,
                                                                            clothesInformation.GenderType, clothesInformation.GenderEntity,
                                                                            clothesInformation.ClothesTypeName, null,
                                                                            clothesInformation.ClothesColorCompositeEntities,
                                                                            clothesInformation.ClothesSizeGroupCompositeEntities);
            var clothesInformationEntityConverter = ClothesInformationEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка цвета одежды
        /// </summary>
        [Fact]
        public void FromEntity_ColorNotFound()
        {
            var clothesInformation = ClothesInformationEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesInformationEntity(clothesInformation, clothesInformation.Description,
                                                                            clothesInformation.GenderType, clothesInformation.GenderEntity,
                                                                            clothesInformation.ClothesTypeName, clothesInformation.ClothesTypeEntity,
                                                                            null,
                                                                            clothesInformation.ClothesSizeGroupCompositeEntities);
            var clothesInformationEntityConverter = ClothesInformationEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка группы размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizeGroupNotFound()
        {
            var clothesInformation = ClothesInformationEntitiesData.ClothesInformationEntities.First();
            var clothesInformationNull = new ClothesInformationEntity(clothesInformation, clothesInformation.Description,
                                                                            clothesInformation.GenderType, clothesInformation.GenderEntity,
                                                                            clothesInformation.ClothesTypeName, clothesInformation.ClothesTypeEntity,
                                                                            clothesInformation.ClothesColorCompositeEntities,
                                                                            null);
            var clothesInformationEntityConverter = ClothesInformationEntityConverter;

            var clothesInformationAfterConverter = clothesInformationEntityConverter.FromEntity(clothesInformationNull);

            Assert.True(clothesInformationAfterConverter.HasErrors);
            Assert.True(clothesInformationAfterConverter.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }

        /// <summary>
        /// Конвертер модели цвета одежды в модель базы данных
        /// </summary>
        private static IClothesInformationEntityConverter ClothesInformationEntityConverter =>
            new ClothesInformationEntityConverter(new ClothesShortEntityConverter(),
                                                  new GenderEntityConverter(),
                                                  new ClothesTypeEntityConverter(new CategoryEntityConverter()),
                                                  new ColorClothesEntityConverter(),
                                                  new SizeGroupEntityConverter(new SizeEntityConverter()));
    }
}