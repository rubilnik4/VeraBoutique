using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public class SizeGroupDomain : SizeGroupShortDomain, ISizeGroupDomain
    {
        public SizeGroupDomain(ISizeGroup sizeGroup, IEnumerable<ISizeDomain> sizes)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizes)
        { }

        public SizeGroupDomain(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<ISizeDomain> sizes)
            : base(clothesSizeType, sizeNormalize)
        {
            Sizes = sizes.ToList().AsReadOnly();
        }

        /// <summary>
        /// Размеры одежды
        /// </summary>
        public IReadOnlyCollection<ISizeDomain> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        public string GetBaseGroupName(SizeType sizeType) =>
            SizeNaming.GetGroupName(sizeType, Sizes);

        #region IEquatable
        public override bool Equals(object? obj) => obj is ISizeGroupDomain sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroupDomain? other) =>
            ((ISizeGroupShortDomain?)other)?.Equals(this) == true &&
            other?.Sizes.SequenceEqual(Sizes) == true;

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Size.GetSizesHashCodes(Sizes));
        #endregion
    }
}