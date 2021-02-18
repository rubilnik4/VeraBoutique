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
    public class SizeGroupMainDomain : SizeGroupMainBase<ISizeDomain>, ISizeGroupMainDomain
    {
        public SizeGroupMainDomain(ISizeGroupBase sizeGroup, IEnumerable<ISizeDomain> sizes)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizes)
        { }

        public SizeGroupMainDomain(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<ISizeDomain> sizes)
            : base(clothesSizeType, sizeNormalize, sizes)
        { }
    }
}