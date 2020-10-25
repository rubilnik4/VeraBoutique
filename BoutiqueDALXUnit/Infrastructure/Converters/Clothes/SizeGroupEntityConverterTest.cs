﻿using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели группы размера одежды в модель базы данных. Тесты
    /// </summary>
    public class SizeGroupEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var sizeGroupDomain = SizeGroupData.GetSizeGroupDomain().First();
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);

            var sizeGroupEntity = sizeGroupEntityConverter.ToEntity(sizeGroupDomain);
            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupEntity);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroupDomain.Equals(sizeGroupAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных. Ошибка размеров одежды
        /// </summary>
        [Fact]
        public void FromEntity_SizesNotFound()
        {
            var sizeGroup = SizeGroupEntitiesData.SizeGroupEntities.First();
            var sizeGroupNull = new SizeGroupEntity(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize,
                                                    null, sizeGroup.ClothesSizeGroupCompositeEntities);
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);

            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupNull);

            Assert.True(sizeGroupAfterConverter.HasErrors);
            Assert.True(sizeGroupAfterConverter.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueNotFound);
        }
    }
}