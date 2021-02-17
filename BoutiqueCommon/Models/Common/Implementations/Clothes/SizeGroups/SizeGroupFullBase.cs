using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Infrastructure.Implementation.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups
{
    /// <summary>
    /// Группа размеров одежды. Базовые данные
    /// </summary>
    public abstract class SizeGroupFullBase<TSize> : SizeGroupBase, ISizeGroupFullBase<TSize>
        where TSize : ISizeBase
    {
        protected SizeGroupFullBase(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<TSize> sizes)
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
        public override bool Equals(object? obj) => obj is ISizeGroupFullBase<TSize> sizeGroup && Equals(sizeGroup);

        public bool Equals(ISizeGroupFullBase<TSize>? other) =>
            base.Equals(other) &&
            other?.Sizes.SequenceEqual(Sizes) == true;

        public override int GetHashCode() => HashCode.Combine(ClothesSizeType, SizeNormalize, SizeBase.GetSizesHashCodes(Sizes));
        #endregion

        /// <summary>
        /// Получить хэш-код группы размеров одежды
        /// </summary>
        public static double GetSizeGroupFullHashCodes<TSizeGroup>(IEnumerable<TSizeGroup> sizeGroups)
            where TSizeGroup : ISizeGroupFullBase<TSize> =>
            sizeGroups.Average(sizeGroup => sizeGroup.GetHashCode());
    }
}
