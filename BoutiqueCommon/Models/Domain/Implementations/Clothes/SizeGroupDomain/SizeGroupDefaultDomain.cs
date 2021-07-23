using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды с размером по умолчанию
    /// </summary>
    public class SizeGroupDefaultDomain : SizeGroupDefaultBase<ISizeDomain>, ISizeGroupDefaultDomain
    {
        public SizeGroupDefaultDomain(ISizeGroupBase sizeGroup, IEnumerable<ISizeDomain> sizes, SizeType defaultSizeType)
           : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizes, defaultSizeType)
        { }

        public SizeGroupDefaultDomain(ISizeGroupMainDomain sizeGroupMain, SizeType defaultSizeType)
            : this(sizeGroupMain.ClothesSizeType, sizeGroupMain.SizeNormalize, sizeGroupMain.Sizes, defaultSizeType)
        { }

        public SizeGroupDefaultDomain(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<ISizeDomain> sizes,
                                      SizeType defaultSizeType)
            : base(clothesSizeType, sizeNormalize, sizes, defaultSizeType)
        { }
    }
}