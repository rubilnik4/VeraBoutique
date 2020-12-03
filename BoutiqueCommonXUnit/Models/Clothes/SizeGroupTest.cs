using System;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
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
        public void SizeGroupShort_Equal_Ok()
        {
            const ClothesSizeType clothesSizeType = ClothesSizeType.Pants;
            const int sizeNormalize = 72;

            var sizeGroupShortDomains = new SizeGroupShortDomain(clothesSizeType, sizeNormalize);

            int sizeGroupHash = HashCode.Combine(clothesSizeType, sizeNormalize);
            Assert.Equal(sizeGroupHash, sizeGroupShortDomains.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void SizeGroup_Equal_Ok()
        {
            const ClothesSizeType clothesSizeType = ClothesSizeType.Pants;
            const int sizeNormalize = 72;
            var sizes = SizeData.SizeDomain;

            var sizeGroupDomains = new SizeGroupDomain(clothesSizeType, sizeNormalize, sizes);

            int sizeGroupHash = HashCode.Combine(clothesSizeType, sizeNormalize, Size.GetSizesHashCodes(sizes));
            Assert.Equal(sizeGroupHash, sizeGroupDomains.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности по внутренним коллекциям
        /// </summary>
        [Fact]
        public void SizeGroupShort_EqualSizes()
        {
            var clothesSizeDomainFirst = SizeGroupData.SizeGroupDomains.First();
            var clothesSizeDomainSecond = SizeGroupData.SizeGroupDomains.First();

            Assert.True(clothesSizeDomainFirst.Equals(clothesSizeDomainSecond));
        }

        /// <summary>
        /// Проверка идентичности по внутренним коллекциям
        /// </summary>
        [Fact]
        public void SizeGroup_EqualSizes()
        {
            var clothesSizeDomainFirst = SizeGroupData.SizeGroupShortDomains.First();
            var clothesSizeDomainSecond = SizeGroupData.SizeGroupShortDomains.First();

            Assert.True(clothesSizeDomainFirst.Equals(clothesSizeDomainSecond));
        }

        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesSizeGroup_ToString()
        {
            const string expectedGroupName = "M (EU 72/74, RU 156/158)";
            const SizeType sizeType = SizeType.American;
            var sizeGroupDomain = SizeGroupData.SizeGroupDomains.First();

            string sizeBaseGroupName = sizeGroupDomain.GetBaseGroupName(sizeType);
            string sizeGroupName = SizeNaming.GetGroupName(sizeType, sizeGroupDomain.Sizes);

            Assert.Equal(expectedGroupName, sizeBaseGroupName);
            Assert.Equal(expectedGroupName, sizeGroupName);
        }
    }
}