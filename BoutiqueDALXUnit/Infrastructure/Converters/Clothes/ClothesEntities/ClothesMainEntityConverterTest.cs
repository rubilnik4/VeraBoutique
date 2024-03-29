﻿using System.Collections.Generic;
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
using BoutiqueDALXUnit.Data.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters.Clothes;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
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
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

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
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

            var clothesDomain = clothesEntityConverter.FromEntity(clothesEntity);

            Assert.True(clothesDomain.OkStatus);
            Assert.True(ClothesData.ClothesMainDomains.First().Equals(clothesDomain.Value));
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка изображений
        /// </summary>
        [Fact]
        public void FromEntity_ImagesNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, null, clothes.Gender, clothes.ClothesType,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(clothesAfterConverter.Errors.First());
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа пола
        /// </summary>
        [Fact]
        public void FromEntity_GenderNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.ClothesImages, null, clothes.ClothesType,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(clothesAfterConverter.Errors.First());
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка типа одежды
        /// </summary>
        [Fact]
        public void FromEntity_ClothesTypeNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.ClothesImages, clothes.Gender, null,
                                               clothes.ClothesColorComposites, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(clothesAfterConverter.Errors.First());
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка цвета одежды
        /// </summary>
        [Fact]
        public void FromEntity_ColorNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.ClothesImages, clothes.Gender, clothes.ClothesType,
                                               null, clothes.ClothesSizeGroupComposites);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(clothesAfterConverter.Errors.First());
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных. Ошибка группы размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizeGroupNotFound()
        {
            var clothes = ClothesEntitiesData.ClothesEntities.First();
            var clothesNull = GetClothesEntity(clothes, clothes.ClothesImages, clothes.Gender, clothes.ClothesType,
                                               clothes.ClothesColorComposites, null);
            var clothesEntityConverter = ClothesEntityConverterMock.ClothesMainEntityConverter;

            var clothesAfterConverter = clothesEntityConverter.FromEntity(clothesNull);

            Assert.True(clothesAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(clothesAfterConverter.Errors.First());
        }

        /// <summary>
        /// Получить сущность одежды
        /// </summary>
        private static ClothesEntity GetClothesEntity(IClothesBase clothes, IEnumerable<ClothesImageEntity>? clothesImages,
                                                      GenderEntity? gender, ClothesTypeEntity? clothesType,
                                                      IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                                                      IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites) =>
            new(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.GenderType, clothes.ClothesTypeName,
                clothesImages, gender, clothesType, clothesColorComposites, clothesSizeGroupComposites);
    }
}