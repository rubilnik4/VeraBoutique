using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Infrastructure.Implementation.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public abstract class SizeGroupMainBase<TSize> : SizeGroupBase, ISizeGroupMainBase<TSize>
        where TSize : ISizeBase
    {
        protected SizeGroupMainBase(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<TSize> sizes)
            : base(clothesSizeType, sizeNormalize)
        {
            Sizes = sizes.ToList().AsReadOnly();
        }

        /// <summary>
        /// Размеры
        /// </summary>
        public IReadOnlyCollection<TSize> Sizes { get; }

        /// <summary>
        /// Получить имя группы размеров по базовому типу
        /// </summary>
        public string GetBaseGroupName(SizeType sizeType) =>
            SizeNaming.GetGroupName(sizeType, Sizes);

        #region IEquatable
        public override bool Equals(object? obj) =>
            obj is ISizeGroupMainBase<TSize> sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroupMainBase<TSize>? other) =>
            base.Equals(other) &&
            other?.Sizes.SequenceEqual(Sizes) == true;

        public override int GetHashCode() =>
            HashCode.Combine(ClothesSizeType, SizeNormalize, Sizes.GetHashCodes());
        #endregion
    }
}
