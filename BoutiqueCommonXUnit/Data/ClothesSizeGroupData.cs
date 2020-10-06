using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using static BoutiqueCommonXUnit.Data.ClothesSizeData;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные группы размера одежды
    /// </summary>
    public static class ClothesSizeGroupData
    {
        /// <summary>
        /// Получить группы размеров одежды
        /// </summary>
        public static List<ISizeGroupDomain> GetClothesSizeGroupDomain() =>
            new List<ISizeGroupDomain>()
            {
                new SizeGroupDomain(ClothesSizeType.Shirt, 46, GetClothesSizeDomain()),
                new SizeGroupDomain(ClothesSizeType.Shirt, 48, GetClothesSizeDomain()),
            };
    }
}