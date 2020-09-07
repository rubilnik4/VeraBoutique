﻿using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters
{
    /// <summary>
    /// Преобразования модели типа пола и модель базы данных. Тесты
    /// </summary>
    public class GenderEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var gender = new GenderDomain(GenderType.Male, "Мужик");
            var genderEntityConverter = new GenderEntityConverter();

            var genderEntity = genderEntityConverter.ToEntity(gender);
            var genderAfterConverter = genderEntityConverter.FromEntity(genderEntity);

            Assert.True(gender.Equals(genderAfterConverter));
        }
    }
}