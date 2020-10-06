using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные размера одежды
    /// </summary>
    public static class ClothesSizeData
    {
        /// <summary>
        /// Получить размеры одежды
        /// </summary>
        public static List<IClothesSizeDomain> GetClothesSizeDomain() =>
            GetClothesSizesAdditionalDomain().Append(GetClothesSizeBaseDomain()).ToList();

        /// <summary>
        /// Получить базовый размер одежды
        /// </summary>
        public static IClothesSizeDomain GetClothesSizeBaseDomain() =>
            new ClothesSizeDomain(SizeType.American, 'M', "M");

        /// <summary>
        /// Получить дополнительные размеры одежды
        /// </summary>
        public static List<IClothesSizeDomain> GetClothesSizesAdditionalDomain() =>
            new List<IClothesSizeDomain>()
            {
                new ClothesSizeDomain(SizeType.European, 73, "72/74"),
                new ClothesSizeDomain(SizeType.Russian, 156, "156/158"),
            };
    }
}