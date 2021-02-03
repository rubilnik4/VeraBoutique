using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueLoader.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы размеров одежды
    /// </summary>
    public static class SizeInitialize
    {
        /// <summary>
        /// Начальные данные таблицы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<ISizeDomain> Sizes =>
            SizeRussian.
            Concat(SizeEuropean).
            Concat(SizeUsa).
            ToList().AsReadOnly();
        
        /// <summary>
        /// Размеры. Россия
        /// </summary>
        public static IReadOnlyCollection<ISizeDomain> SizeRussian =>
            new List<SizeDomain>
            {
                new (SizeType.Russian, "35"),
                new (SizeType.Russian, "36"),
                new (SizeType.Russian, "37"),
                new (SizeType.Russian, "38"),
                new (SizeType.Russian, "39"),
                new (SizeType.Russian, "40"),
                new (SizeType.Russian, "41"),
                new (SizeType.Russian, "42"),
                new (SizeType.Russian, "43"),
                new (SizeType.Russian, "44"),
                new (SizeType.Russian, "45"),
                new (SizeType.Russian, "46"),
                new (SizeType.Russian, "48"),
                new (SizeType.Russian, "50"),
                new (SizeType.Russian, "52"),
                new (SizeType.Russian, "54"),
                new (SizeType.Russian, "56"),
                new (SizeType.Russian, "58"),
                new (SizeType.Russian, "60"),
            };

        /// <summary>
        /// Размеры. Европа
        /// </summary>
        public static IReadOnlyCollection<ISizeDomain> SizeEuropean =>
            new List<SizeDomain>
            {
                new (SizeType.European, "2"),
                new (SizeType.European, "3"),
                new (SizeType.European, "4"),
                new (SizeType.European, "5"),
                new (SizeType.European, "6"),
                new (SizeType.European, "7"),
                new (SizeType.European, "8"),
                new (SizeType.European, "32"),
                new (SizeType.European, "33"),
                new (SizeType.European, "34"),
                new (SizeType.European, "35"),
                new (SizeType.European, "36"),
                new (SizeType.European, "37"),
                new (SizeType.European, "38"),
                new (SizeType.European, "39"),
                new (SizeType.European, "40"),
                new (SizeType.European, "41"),
                new (SizeType.European, "42"),
                new (SizeType.European, "43"),
                new (SizeType.European, "44"),
                new (SizeType.European, "45"),
                new (SizeType.European, "46"),
                new (SizeType.European, "47"),
                new (SizeType.European, "48"),
                new (SizeType.European, "49"),
                new (SizeType.European, "50"),
                new (SizeType.European, "52"),
                new (SizeType.European, "54"),
                new (SizeType.European, "56"),
                new (SizeType.European, "58"),
                new (SizeType.European, "60"),
                new (SizeType.European, "62"),
            };

        /// <summary>
        /// Размеры. США
        /// </summary>
        public static IReadOnlyCollection<ISizeDomain> SizeUsa =>
            new List<SizeDomain>
            {
                new (SizeType.American, "3"),
                new (SizeType.American, "4"),
                new (SizeType.American, "5"),
                new (SizeType.American, "6"),
                new (SizeType.American, "7"),
                new (SizeType.American, "8"),
                new (SizeType.American, "9"),
                new (SizeType.American, "9.5"),
                new (SizeType.American, "10"),
                new (SizeType.American, "10.5"),
                new (SizeType.American, "11"),
                new (SizeType.American, "11.5"),
                new (SizeType.American, "12"),
                new (SizeType.American, "12.5"),
                new (SizeType.American, "14"),
                new (SizeType.American, "16"),
                new (SizeType.American, "18"),
                new (SizeType.American, "20"),
                new (SizeType.American, "22"),
                new (SizeType.American, "24"),
                new (SizeType.American, "26"),
                new (SizeType.American, "28"),
                new (SizeType.American, "30"),
                new (SizeType.American, "32"),
                new (SizeType.American, "34"),
                new (SizeType.American, "36"),
                new (SizeType.American, "38"),
                new (SizeType.American, "40"),
                new (SizeType.American, "42"),
                new (SizeType.American, "44"),
                new (SizeType.American, "46"),
                new (SizeType.American, "48"),
                new (SizeType.American, "50"),
                new (SizeType.American, "XXS"),
                new (SizeType.American, "XS"),
                new (SizeType.American, "S"),
                new (SizeType.American, "M"),
                new (SizeType.American, "L"),
                new (SizeType.American, "XL"),
                new (SizeType.American, "XXL"),
                new (SizeType.American, "XXXL"),
            };
    }
}