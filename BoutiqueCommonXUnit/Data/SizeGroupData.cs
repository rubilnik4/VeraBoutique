using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using static BoutiqueCommonXUnit.Data.SizeData;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные группы размера одежды
    /// </summary>
    public static class SizeGroupData
    {
        /// <summary>
        /// Получить группы размеров одежды
        /// </summary>
        public static List<ISizeGroupDomain> GetSizeGroupDomain() =>
            new List<ISizeGroupDomain>()
            {
                new SizeGroupDomain(ClothesSizeType.Shirt, 46, GetSizeDomain()),
                new SizeGroupDomain(ClothesSizeType.Shirt, 48, GetSizeDomain()),
            };
    }
}