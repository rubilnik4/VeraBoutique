using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные группы размера одежды
    /// </summary>
    public static class SizeGroupData
    {
        /// <summary>
        /// Получить группы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<ISizeGroupDomain> SizeGroupDomain =>
            new List<ISizeGroupDomain>()
            {
                new SizeGroupDomain(ClothesSizeType.Shirt, 46, SizeData.SizeDomain),
                new SizeGroupDomain(ClothesSizeType.Shirt, 48, SizeData.SizeDomain),
            };
    }
}