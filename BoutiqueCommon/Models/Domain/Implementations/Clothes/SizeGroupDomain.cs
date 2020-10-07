﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public class SizeGroupDomain : SizeGroup<ISizeDomain>, ISizeGroupDomain
    {
        public SizeGroupDomain(ClothesSizeType clothesSizeType, int sizeNormalize,
                               IEnumerable<ISizeDomain> sizes)
            : base(clothesSizeType, sizeNormalize)
        {
            Sizes = sizes.ToList().AsReadOnly();
        }

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public override IReadOnlyCollection<ISizeDomain> Sizes { get; }
    }
}