using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные
    /// </summary>
    public class SizeGroupShortDomain : SizeGroup, ISizeGroupShortDomain
    {
        public SizeGroupShortDomain(ISizeGroup sizeGroup)
       : base(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }

        public SizeGroupShortDomain(ClothesSizeType clothesSizeType, int sizeNormalize)
         : base(clothesSizeType, sizeNormalize)
        { }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISizeGroupShortDomain sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroupShortDomain? other) =>
            base.Equals(other);

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize);
        #endregion
    }
}