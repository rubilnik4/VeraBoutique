using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные
    /// </summary>
    public class SizeGroupShortDomain : SizeGroupShortBase, ISizeGroupShortDomain
    {
        public SizeGroupShortDomain(ISizeGroupShortBase sizeGroup)
            : base(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }

        public SizeGroupShortDomain(ClothesSizeType clothesSizeType, int sizeNormalize)
         : base(clothesSizeType, sizeNormalize)
        { }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id => GetIdHashCode(ClothesSizeType, SizeNormalize);
    }
}