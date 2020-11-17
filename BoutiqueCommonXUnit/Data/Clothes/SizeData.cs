using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные размера одежды
    /// </summary>
    public static class SizeData
    {
        /// <summary>
        /// Получить размеры одежды
        /// </summary>
        public static List<ISizeDomain> SizeDomain =>
            new List<ISizeDomain>()
            {
                new SizeDomain(SizeType.American, "M"),
                new SizeDomain(SizeType.European, "72/74"),
                new SizeDomain(SizeType.Russian,  "156/158"),
            };
    }
}