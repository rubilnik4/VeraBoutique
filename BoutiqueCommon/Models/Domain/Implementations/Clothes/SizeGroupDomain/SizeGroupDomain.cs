using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public class SizeGroupDomain : SizeGroupBase<ISizeDomain>, ISizeGroupDomain
    {
        public SizeGroupDomain(ISizeGroupShortBase sizeGroupShort, IEnumerable<ISizeDomain> sizes)
            : this(sizeGroupShort.ClothesSizeType, sizeGroupShort.SizeNormalize, sizes)
        { }

        public SizeGroupDomain(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<ISizeDomain> sizes)
            : base(clothesSizeType, sizeNormalize, sizes)
        { }
    }
}