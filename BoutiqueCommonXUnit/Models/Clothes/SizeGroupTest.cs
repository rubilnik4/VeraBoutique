using System;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
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
        public void ClothesSizeGroup_ToString()
        {
            const string expectedGroupName = "M (EU 72/74, RU 156/158)";
            const SizeType sizeType = SizeType.Russian;
            var sizeGroupDomain = ClothesSizeGroupData.GetClothesSizeGroupDomain().First();

            string sizeBaseGroupName = sizeGroupDomain.GetBaseGroupName(sizeType);
            string sizeGroupName = SizeGroup.GetGroupName(sizeType, sizeGroupDomain.Sizes);

            Assert.Equal(sizeBaseGroupName, sizeGroupName);
            Assert.Equal(expectedGroupName, sizeGroupName);
        }
    }
}