﻿using System.Collections.Generic;
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
        public static List<ISizeDomain> GetClothesSizeDomain() =>
            new List<ISizeDomain>()
            {
                new SizeDomain(SizeType.American, 'M', "M"),
                new SizeDomain(SizeType.European, 73, "72/74"),
                new SizeDomain(SizeType.Russian, 156, "156/158"),
            };
    }
}