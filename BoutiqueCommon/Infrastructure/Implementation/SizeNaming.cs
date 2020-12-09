using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Infrastructure.Implementation
{
    /// <summary>
    /// Именование группы размеров одежды
    /// </summary>
    public static class SizeNaming
    {
        /// <summary>
        /// Получить укороченное наименование размера
        /// </summary>
        public static string GetSizeNameShort(SizeType clothesSizeType, string sizeName) =>
             $"{GetSizeTypeShort(clothesSizeType)} {sizeName}".Trim();

        /// <summary>
        /// Получить имя группы
        /// </summary>
        public static string GetGroupName<TSize>(SizeType sizeTypeBase,
                                          IReadOnlyCollection<TSize> sizes)
            where TSize :ISizeBase =>
            (sizes.FirstOrDefault(size => size.SizeType == sizeTypeBase)?.ToString() ?? String.Empty) +
            GetSizeGroupSubName(sizes.Where(size => size.SizeType != sizeTypeBase));

        /// <summary>
        /// Укороченное наименование типа размера
        /// </summary>
        private static string GetSizeTypeShort(SizeType clothesSizeType) =>
            clothesSizeType switch
            {
                SizeType.American => "",
                SizeType.European => "EU",
                SizeType.Russian => "RU",
                _ => throw new InvalidEnumArgumentException(nameof(SizeType), (int)clothesSizeType, typeof(SizeType))
            };

        /// <summary>
        /// Получить наименование дополнительной группы размеров
        /// </summary>
        private static string GetSizeGroupSubName<TSize>(IEnumerable<TSize> sizesAdditional)
            where TSize : ISizeBase =>
            sizesAdditional.ToList().
            WhereContinue(clothesSize => clothesSize.Count > 0,
                okFunc: clothesSizeCollection => $" ({String.Join(", ", clothesSizeCollection)})",
                badFunc: _ => String.Empty);
    }
}