using System;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
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
            var sizes = SizeData.SizeDomain;

            var clothesSizeDomain = new SizeGroupDomain(clothesSizeType, sizeNormalize, sizes);

            int genderHash = HashCode.Combine(clothesSizeType, sizeNormalize, Size.GetSizesHashCodes(sizes));
            Assert.Equal(genderHash, clothesSizeDomain.GetHashCode());
        }

        /// <summary>
        /// Проверка идентичности по внутренним коллекциям
        /// </summary>
        [Fact]
        public void SizeGroup_EqualSizes()
        {
            const ClothesSizeType clothesSizeType = ClothesSizeType.Pants;
            const int sizeNormalize = 72;
            
            var clothesSizeDomainFirst = new SizeGroupDomain(clothesSizeType, sizeNormalize, SizeData.SizeDomain);
            var clothesSizeDomainSecond = new SizeGroupDomain(clothesSizeType, sizeNormalize, SizeData.SizeDomain);

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
            var sizeGroupDomain = SizeGroupData.SizeGroupDomain.First();

            string sizeBaseGroupName = sizeGroupDomain.GetBaseGroupName(sizeType);
            string sizeGroupName = SizeNaming.GetGroupName(sizeType, sizeGroupDomain.Sizes);

            Assert.Equal(expectedGroupName, sizeBaseGroupName);
            Assert.Equal(expectedGroupName, sizeGroupName);
        }
    }
}