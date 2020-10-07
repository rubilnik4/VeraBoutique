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
            SizeOuterwearData.
            Concat(SizeShirtData).
            ToList().AsReadOnly();

        /// <summary>
        /// Размеры верхней одежды
        /// </summary>
        public static IReadOnlyCollection<SizeEntity> SizeOuterwearData =>
         new List<SizeEntity>
            {
                new SizeEntity(SizeType.European, 44, (ClothesSizeType.Outerwear, 44)),
                new SizeEntity(SizeType.American, 34, (ClothesSizeType.Outerwear, 44)),

                new SizeEntity(SizeType.European, 46, (ClothesSizeType.Outerwear, 46)),
                new SizeEntity(SizeType.American, 36, (ClothesSizeType.Outerwear, 46)),

                new SizeEntity(SizeType.European, 48, (ClothesSizeType.Outerwear, 48)),
                new SizeEntity(SizeType.American, 38, (ClothesSizeType.Outerwear, 48)),

                new SizeEntity(SizeType.European, 50, (ClothesSizeType.Outerwear, 50)),
                new SizeEntity(SizeType.American, 40, (ClothesSizeType.Outerwear, 50)),

                new SizeEntity(SizeType.European, 52, (ClothesSizeType.Outerwear, 52)),
                new SizeEntity(SizeType.American, 42, (ClothesSizeType.Outerwear, 52)),

                new SizeEntity(SizeType.European, 54, (ClothesSizeType.Outerwear, 54)),
                new SizeEntity(SizeType.American, 44, (ClothesSizeType.Outerwear, 54)),

                new SizeEntity(SizeType.European, 56, (ClothesSizeType.Outerwear, 56)),
                new SizeEntity(SizeType.American, 46, (ClothesSizeType.Outerwear, 56)),

                new SizeEntity(SizeType.European, 58, (ClothesSizeType.Outerwear, 58)),
                new SizeEntity(SizeType.American, 48, (ClothesSizeType.Outerwear, 58)),

                new SizeEntity(SizeType.European, 60, (ClothesSizeType.Outerwear, 60)),
                new SizeEntity(SizeType.American, 50, (ClothesSizeType.Outerwear, 60)),
            }.AsReadOnly();

        /// <summary>
        /// Размеры верхней одежды
        /// </summary>
        public static IReadOnlyCollection<SizeEntity> SizeShirtData =>
         new List<SizeEntity>
            {
                new SizeEntity(SizeType.American, 'S', "S", (ClothesSizeType.Shirt, 46)),
                new SizeEntity(SizeType.European, 38, "37-38", (ClothesSizeType.Shirt, 46)),
                new SizeEntity(SizeType.Russian, 38, "44-46", (ClothesSizeType.Shirt, 46)),

                new SizeEntity(SizeType.American, 'M', "M", (ClothesSizeType.Shirt, 48)),
                new SizeEntity(SizeType.European, 40, "39-40", (ClothesSizeType.Shirt, 48)),
                new SizeEntity(SizeType.Russian, 48, (ClothesSizeType.Shirt, 48)),

                new SizeEntity(SizeType.American, 'L', "L", (ClothesSizeType.Shirt, 52)),
                new SizeEntity(SizeType.European, 42, "41-42", (ClothesSizeType.Shirt, 52)),
                new SizeEntity(SizeType.Russian, 52, "51-52", (ClothesSizeType.Shirt, 52)),

                new SizeEntity(SizeType.American, 'X'+'L', "XL", (ClothesSizeType.Shirt, 54)),
                new SizeEntity(SizeType.European, 44, "43-44", (ClothesSizeType.Shirt, 54)),
                new SizeEntity(SizeType.Russian, 54, (ClothesSizeType.Shirt, 54)),

                new SizeEntity(SizeType.American, 'X'+ 'X'+'L', "XXL", (ClothesSizeType.Shirt, 56)),
                new SizeEntity(SizeType.European, 46, "45-46", (ClothesSizeType.Shirt, 56)),
                new SizeEntity(SizeType.Russian, 56, (ClothesSizeType.Shirt, 56)),

                new SizeEntity(SizeType.American, 'X'+ 'X'+ 'X'+'L', "XXL", (ClothesSizeType.Shirt, 62)),
                new SizeEntity(SizeType.European, 46, "45-46", (ClothesSizeType.Shirt, 62)),
                new SizeEntity(SizeType.Russian, 62, "58-62", (ClothesSizeType.Shirt, 62)),
            }.AsReadOnly();
    }
}