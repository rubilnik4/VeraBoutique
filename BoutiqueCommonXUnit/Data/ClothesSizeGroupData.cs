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
        public static List<IClothesSizeGroupDomain> GetClothesSizeGroupDomain() =>
            new List<IClothesSizeGroupDomain>()
            {
                new ClothesSizeGroupDomain(GetClothesSizeBaseDomain(),
                                           GetClothesSizesAdditionalDomain()),
                new ClothesSizeGroupDomain(GetClothesSizeBaseDomain(),
                                           GetClothesSizesAdditionalDomain()),
            };
    }
}