using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы размеров одежды
    /// </summary>
    public static class SizeInitialize
    {
        /// <summary>
        /// Начальные данные таблицы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<SizeEntity> SizeData =>
            SizeRussianData.
            Concat(SizeEuropeanData).
            Concat(SizeUsaData).
            ToList().AsReadOnly();
        
        /// <summary>
        /// Размеры. Россия
        /// </summary>
        public static IReadOnlyCollection<SizeEntity> SizeRussianData =>
            new List<SizeEntity>
            {
                new SizeEntity(SizeType.Russian, "35"),
                new SizeEntity(SizeType.Russian, "36"),
                new SizeEntity(SizeType.Russian, "37"),
                new SizeEntity(SizeType.Russian, "38"),
                new SizeEntity(SizeType.Russian, "39"),
                new SizeEntity(SizeType.Russian, "40"),
                new SizeEntity(SizeType.Russian, "41"),
                new SizeEntity(SizeType.Russian, "42"),
                new SizeEntity(SizeType.Russian, "43"),
                new SizeEntity(SizeType.Russian, "44"),
                new SizeEntity(SizeType.Russian, "45"),
                new SizeEntity(SizeType.Russian, "46"),
                new SizeEntity(SizeType.Russian, "48"),
                new SizeEntity(SizeType.Russian, "50"),
                new SizeEntity(SizeType.Russian, "52"),
                new SizeEntity(SizeType.Russian, "54"),
                new SizeEntity(SizeType.Russian, "56"),
                new SizeEntity(SizeType.Russian, "58"),
                new SizeEntity(SizeType.Russian, "60"),
            };

        /// <summary>
        /// Размеры. Европа
        /// </summary>
        public static IReadOnlyCollection<SizeEntity> SizeEuropeanData =>
            new List<SizeEntity>
            {
                new SizeEntity(SizeType.European, "2"),
                new SizeEntity(SizeType.European, "3"),
                new SizeEntity(SizeType.European, "4"),
                new SizeEntity(SizeType.European, "5"),
                new SizeEntity(SizeType.European, "6"),
                new SizeEntity(SizeType.European, "7"),
                new SizeEntity(SizeType.European, "8"),
                new SizeEntity(SizeType.European, "32"),
                new SizeEntity(SizeType.European, "33"),
                new SizeEntity(SizeType.European, "34"),
                new SizeEntity(SizeType.European, "35"),
                new SizeEntity(SizeType.European, "36"),
                new SizeEntity(SizeType.European, "37"),
                new SizeEntity(SizeType.European, "38"),
                new SizeEntity(SizeType.European, "39"),
                new SizeEntity(SizeType.European, "40"),
                new SizeEntity(SizeType.European, "41"),
                new SizeEntity(SizeType.European, "42"),
                new SizeEntity(SizeType.European, "43"),
                new SizeEntity(SizeType.European, "44"),
                new SizeEntity(SizeType.European, "45"),
                new SizeEntity(SizeType.European, "46"),
                new SizeEntity(SizeType.European, "47"),
                new SizeEntity(SizeType.European, "48"),
                new SizeEntity(SizeType.European, "49"),
                new SizeEntity(SizeType.European, "50"),
                new SizeEntity(SizeType.European, "52"),
                new SizeEntity(SizeType.European, "54"),
                new SizeEntity(SizeType.European, "56"),
                new SizeEntity(SizeType.European, "58"),
                new SizeEntity(SizeType.European, "60"),
                new SizeEntity(SizeType.European, "62"),
            };

        /// <summary>
        /// Размеры. США
        /// </summary>
        public static IReadOnlyCollection<SizeEntity> SizeUsaData =>
            new List<SizeEntity>
            {
                new SizeEntity(SizeType.American, "3"),
                new SizeEntity(SizeType.American, "4"),
                new SizeEntity(SizeType.American, "5"),
                new SizeEntity(SizeType.American, "6"),
                new SizeEntity(SizeType.American, "7"),
                new SizeEntity(SizeType.American, "8"),
                new SizeEntity(SizeType.American, "9"),
                new SizeEntity(SizeType.American, "9.5"),
                new SizeEntity(SizeType.American, "10"),
                new SizeEntity(SizeType.American, "10.5"),
                new SizeEntity(SizeType.American, "11"),
                new SizeEntity(SizeType.American, "11.5"),
                new SizeEntity(SizeType.American, "12"),
                new SizeEntity(SizeType.American, "12.5"),
                new SizeEntity(SizeType.American, "14"),
                new SizeEntity(SizeType.American, "16"),
                new SizeEntity(SizeType.American, "18"),
                new SizeEntity(SizeType.American, "20"),
                new SizeEntity(SizeType.American, "22"),
                new SizeEntity(SizeType.American, "24"),
                new SizeEntity(SizeType.American, "26"),
                new SizeEntity(SizeType.American, "28"),
                new SizeEntity(SizeType.American, "30"),
                new SizeEntity(SizeType.American, "32"),
                new SizeEntity(SizeType.American, "34"),
                new SizeEntity(SizeType.American, "36"),
                new SizeEntity(SizeType.American, "38"),
                new SizeEntity(SizeType.American, "40"),
                new SizeEntity(SizeType.American, "42"),
                new SizeEntity(SizeType.American, "44"),
                new SizeEntity(SizeType.American, "46"),
                new SizeEntity(SizeType.American, "48"),
                new SizeEntity(SizeType.American, "50"),
                new SizeEntity(SizeType.American, "XXS"),
                new SizeEntity(SizeType.American, "XS"),
                new SizeEntity(SizeType.American, "S"),
                new SizeEntity(SizeType.American, "M"),
                new SizeEntity(SizeType.American, "L"),
                new SizeEntity(SizeType.American, "XL"),
                new SizeEntity(SizeType.American, "XXL"),
                new SizeEntity(SizeType.American, "XXXL"),
            };
    }
}