using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Infrastructure.Implementation.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public abstract class SizeGroupBase<TSize> : SizeGroupShortBase, ISizeGroupBase<TSize>
        where TSize : ISizeBase
    {
        protected SizeGroupBase(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<TSize> sizes)
            : base(clothesSizeType, sizeNormalize)
        {
            Sizes = sizes.ToList();
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
        public override bool Equals(object? obj) => obj is ISizeGroupBase<TSize> sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroupBase<TSize>? other) =>
            base.Equals(other) &&
            other?.Sizes.SequenceEqual(Sizes) == true;

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize, SizeBase.GetSizesHashCodes(Sizes));
        #endregion

        /// <summary>
        /// Получить хэш-код группы размеров одежды
        /// </summary>
        public static double GetSizeGroupHashCodes<TSizeGroup>(IEnumerable<TSizeGroup> sizeGroups)
            where TSizeGroup : ISizeGroupBase<TSize> =>
            sizeGroups.Average(color => color.GetHashCode());
    }
}
