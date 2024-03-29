﻿using System;
using System.Linq;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Infrastructure.Implementation.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа. Тесты
    /// </summary>
    public class SizeGroupTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void SizeGroup_Equal_Ok()
        {
            const ClothesSizeType clothesSizeType = ClothesSizeType.Pants;
            const int sizeNormalize = 72;

            var sizeGroupDomains = new SizeGroupDomain(clothesSizeType, sizeNormalize);

            int sizeGroupHash = HashCode.Combine(clothesSizeType, sizeNormalize);
            Assert.Equal(sizeGroupHash, sizeGroupDomains.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности по внутренним коллекциям
        /// </summary>
        [Fact]
        public void SizeGroup_EqualSizes()
        {
            var first = SizeGroupData.SizeGroupDomains.First();
            var second = new SizeGroupDomain(first.ClothesSizeType, first.SizeNormalize);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка идентичности по внутренним коллекциям
        /// </summary>
        [Fact]
        public void SizeGroupMain_EqualSizes()
        {
            var first = SizeGroupData.SizeGroupMainDomains.First();
            var second = new SizeGroupMainDomain(first.ClothesSizeType, first.SizeNormalize, first.Sizes);

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Проверка перевода в строку
        /// </summary>
        [Fact]
        public void ClothesSizeGroup_ToString()
        {
            const string expectedGroupName = "M (EU 72/74, RU 156/158)";
            const SizeType sizeType = SizeType.American;
            var sizeGroupMainDomain = SizeGroupData.SizeGroupMainDomains.First();

            string sizeBaseGroupName = sizeGroupMainDomain.GetBaseGroupName(sizeType);
            string sizeGroupName = SizeNaming.GetGroupName(sizeType, sizeGroupMainDomain.Sizes);

            Assert.Equal(expectedGroupName, sizeBaseGroupName);
            Assert.Equal(expectedGroupName, sizeGroupName);
        }

        /// <summary>
        /// Проверка перевода в строку
        /// </summary>
        [Fact]
        public void ClothesSizeGroupDefault_ToString()
        {
            const SizeType sizeType = SizeType.American;
            var sizeGroupMainDomain= SizeGroupData.SizeGroupMainDomains.First();
            var sizeGroupDefaultDomain = new SizeGroupDefaultDomain(sizeGroupMainDomain, sizeType);

            string sizeGroupName = sizeGroupDefaultDomain.ToString();

            Assert.Equal(sizeGroupName, sizeGroupMainDomain.GetBaseGroupName(sizeType));
        }
    }
}