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
    public class ClothesSizeGroupTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void ClothesSizeGroup_ToString()
        {
            var expectedGroupName = "M (EU 72/74, RU 156/158)";
            var clothesSizeGroupDomain = ClothesSizeGroupData.GetClothesSizeGroupDomain().First();

            string clothesSizeGroupToString = clothesSizeGroupDomain.ToString();
            string clothesSizeGroupName = SizeGroup.GetGroupName(clothesSizeGroupDomain.ClothesSizeBase, 
                                                                        clothesSizeGroupDomain.Sizes);

            Assert.Equal(expectedGroupName, clothesSizeGroupToString);
            Assert.Equal(expectedGroupName, clothesSizeGroupName);
        }
    }
}