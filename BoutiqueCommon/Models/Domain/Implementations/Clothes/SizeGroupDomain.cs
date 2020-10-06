﻿using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public class SizeGroupDomain : SizeGroup, ISizeGroupDomain
    {
        public SizeGroupDomain(ClothesSizeType clothesSizeType, int sizeNormalize,
                                      IReadOnlyCollection<ISize> sizes)
            : base(clothesSizeType, sizeNormalize, sizes)
        { }
    }
}