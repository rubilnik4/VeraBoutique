using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы группы размеров одежды
    /// </summary>
    public static class SizeGroupInitialize
    {
        /// <summary>
        /// Начальные данные связующей таблицы группы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<ISizeGroupDomain> SizeGroups =>
            OutwearSizes.
            Concat(ShirtSizes).
            Concat(TshirtSizes).
            Concat(JacketSizes).
            Concat(PantsSizes).
            Concat(UnderwearSizes).
            Concat(SocksSizes).
            Concat(ShoesSizes).
            Concat(DressSizes).
            Concat(BlouseSizes).
            ToList().AsReadOnly();

        /// <summary>
        /// Связующие размеры. Верхняя одежда
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> OutwearSizes =>
            new List<SizeGroupDomain>
            {
                 new(ClothesSizeType.Outerwear, 38,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "38"),
                        new(SizeType.European, "38"),
                        new(SizeType.American, "28"),
                     }),
                 new(ClothesSizeType.Outerwear, 40,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "40"),
                        new(SizeType.European, "40"),
                        new(SizeType.American, "30"),
                     }),
                 new(ClothesSizeType.Outerwear, 42,
                     new List<SizeDomain>
                     {
                         new(SizeType.Russian, "42"),
                         new(SizeType.European, "42"),
                         new(SizeType.American, "32"),
                     }),
                  new(ClothesSizeType.Outerwear, 44,
                     new List<SizeDomain>
                     {
                         new(SizeType.Russian, "44"),
                         new(SizeType.European, "44"),
                         new(SizeType.American, "34"),
                     }),
                  new(ClothesSizeType.Outerwear, 46,
                     new List<SizeDomain>
                     {
                         new(SizeType.Russian, "46"),
                         new(SizeType.European, "46"),
                         new(SizeType.American, "36"),
                     }),
                  new(ClothesSizeType.Outerwear, 48,
                     new List<SizeDomain>
                     {
                         new(SizeType.Russian, "48"),
                         new(SizeType.European, "48"),
                         new(SizeType.American, "38"),
                     }),
                  new(ClothesSizeType.Outerwear, 50,
                      new List<SizeDomain>
                      {
                          new(SizeType.Russian, "50"),
                          new(SizeType.European, "50"),
                          new(SizeType.American, "40"),
                      }),
                   new(ClothesSizeType.Outerwear, 52,
                      new List<SizeDomain>
                      {
                          new(SizeType.Russian, "52"),
                          new(SizeType.European, "52"),
                          new(SizeType.American, "42"),
                      }),
                   new(ClothesSizeType.Outerwear, 54,
                      new List<SizeDomain>
                      {
                          new(SizeType.Russian, "54"),
                          new(SizeType.European, "54"),
                          new(SizeType.American, "44"),
                      }),
                   new(ClothesSizeType.Outerwear, 56,
                      new List<SizeDomain>
                      {
                          new(SizeType.Russian, "56"),
                          new(SizeType.European, "56"),
                          new(SizeType.American, "46"),
                      }),
                    new(ClothesSizeType.Outerwear, 58,
                      new List<SizeDomain>
                      {
                          new(SizeType.Russian, "58"),
                          new(SizeType.European, "58"),
                          new(SizeType.American, "48"),
                      }),
                    new(ClothesSizeType.Outerwear, 60,
                      new List<SizeDomain>
                      {
                          new(SizeType.Russian, "60"),
                          new(SizeType.European, "60"),
                          new(SizeType.American, "50"),
                      }),
            };

        /// <summary>
        /// Связующие размеры. Рубашки
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> ShirtSizes =>
            new List<SizeGroupDomain>
            {
                 new(ClothesSizeType.Shirt, 44,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "44"),
                        new(SizeType.European, "38"),
                        new(SizeType.American, "S"),
                     }),
                  new(ClothesSizeType.Shirt, 48,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "48"),
                        new(SizeType.European, "40"),
                        new(SizeType.American, "M"),
                     }),
                  new(ClothesSizeType.Shirt, 50,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "50"),
                        new(SizeType.European, "42"),
                        new(SizeType.American, "L"),
                     }),
                  new(ClothesSizeType.Shirt, 54,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "54"),
                        new(SizeType.European, "44"),
                        new(SizeType.American, "XL"),
                     }),
                  new(ClothesSizeType.Shirt, 56,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "56"),
                        new(SizeType.European, "46"),
                        new(SizeType.American, "XXL"),
                     }),
                   new(ClothesSizeType.Shirt, 58,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "58"),
                        new(SizeType.European, "48"),
                        new(SizeType.American, "XXXL"),
                     }),
            };

        /// <summary>
        /// Связующие размеры. Футболки
        /// </summary>
        public static IReadOnlyCollection<ISizeGroupDomain> TshirtSizes =>
            new List<SizeGroupDomain>
            {
                 new(ClothesSizeType.Tshirt, 42,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "42"),
                        new(SizeType.European, "42"),
                        new(SizeType.American, "XXS"),
                     }),
                 new(ClothesSizeType.Tshirt, 44,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "44"),
                        new(SizeType.European, "44"),
                        new(SizeType.American, "XS"),
                     }),
                  new(ClothesSizeType.Tshirt, 46,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "46"),
                        new(SizeType.European, "46"),
                        new(SizeType.American, "S"),
                     }),
                  new(ClothesSizeType.Tshirt, 48,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "48"),
                        new(SizeType.European, "50"),
                        new(SizeType.American, "M"),
                     }),
                   new(ClothesSizeType.Tshirt, 50,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "50"),
                        new(SizeType.European, "54"),
                        new(SizeType.American, "L"),
                     }),
                   new(ClothesSizeType.Tshirt, 52,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "52"),
                        new(SizeType.European, "58"),
                        new(SizeType.American, "XL"),
                     }),
                   new(ClothesSizeType.Tshirt, 54,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "54"),
                        new(SizeType.European, "60"),
                        new(SizeType.American, "XXL"),
                     }),
                   new(ClothesSizeType.Tshirt, 56,
                     new List<SizeDomain>
                     {
                        new(SizeType.Russian, "56"),
                        new(SizeType.European, "62"),
                        new(SizeType.American, "XXXL"),
                     }),
            };

        /// <summary>
        /// Связующие размеры. Куртки
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> JacketSizes =>
            new List<SizeGroupDomain>
            {
                new(ClothesSizeType.Jacket, 38,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "38"),
                        new(SizeType.European, "32"),
                        new(SizeType.American, "4"),
                    }),
                new(ClothesSizeType.Jacket, 40,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "40"),
                        new(SizeType.European, "34"),
                        new(SizeType.American, "6"),
                    }),
                 new(ClothesSizeType.Jacket, 42,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "42"),
                        new(SizeType.European, "36"),
                        new(SizeType.American, "8"),
                    }),
                 new(ClothesSizeType.Jacket, 44,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "44"),
                        new(SizeType.European, "38"),
                        new(SizeType.American, "10"),
                    }),
                 new(ClothesSizeType.Jacket, 46,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "46"),
                        new(SizeType.European, "40"),
                        new(SizeType.American, "12"),
                    }),
                 new(ClothesSizeType.Jacket, 48,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "48"),
                        new(SizeType.European, "42"),
                        new(SizeType.American, "14"),
                    }),
                 new(ClothesSizeType.Jacket, 50,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "50"),
                        new(SizeType.European, "44"),
                        new(SizeType.American, "16"),
                    }),
                 new(ClothesSizeType.Jacket, 52,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "52"),
                        new(SizeType.European, "46"),
                        new(SizeType.American, "18"),
                    }),
                 new(ClothesSizeType.Jacket, 54,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "54"),
                        new(SizeType.European, "48"),
                        new(SizeType.American, "20"),
                    }),
                 new(ClothesSizeType.Jacket, 56,
                    new List<SizeDomain>
                    {
                        new(SizeType.Russian, "56"),
                        new(SizeType.European, "50"),
                        new(SizeType.American, "22"),
                    }),
            };

        /// <summary>
        /// Связующие размеры. Штаны
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> PantsSizes =>
            new List<SizeGroupDomain>
            {
                new (ClothesSizeType.Pants, 44,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "44"),
                         new (SizeType.European, "38"),
                         new (SizeType.American, "XXS"),
                  }),
                new (ClothesSizeType.Pants, 46,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "46"),
                         new (SizeType.European, "40"),
                         new (SizeType.American, "XS"),
                  }),
                new (ClothesSizeType.Pants, 48,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "48"),
                         new (SizeType.European, "42"),
                         new (SizeType.American, "S"),
                  }),
                new (ClothesSizeType.Pants, 50,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "50"),
                         new (SizeType.European, "44"),
                         new (SizeType.American, "M"),
                  }),
                 new (ClothesSizeType.Pants, 52,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "52"),
                         new (SizeType.European, "46"),
                         new (SizeType.American, "L"),
                  }),
                 new (ClothesSizeType.Pants, 54,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "54"),
                         new (SizeType.European, "48"),
                         new (SizeType.American, "XL"),
                  }),
                 new (ClothesSizeType.Pants, 56,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "56"),
                         new (SizeType.European, "50"),
                         new (SizeType.American, "XXL"),
                  }),
            };

        /// <summary>
        /// Связующие размеры. Нижнее белье
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> UnderwearSizes =>
            new List<SizeGroupDomain>
            {
                new (ClothesSizeType.Underwear, 42,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "42"),
                         new (SizeType.European, "2"),
                         new (SizeType.American, "XS"),
                     }),
                new (ClothesSizeType.Underwear, 44,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "44"),
                         new (SizeType.European, "3"),
                         new (SizeType.American, "S"),
                     }),
                new (ClothesSizeType.Underwear, 46,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "46"),
                         new (SizeType.European, "4"),
                         new (SizeType.American, "M"),
                     }),
                new (ClothesSizeType.Underwear, 48,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "48"),
                         new (SizeType.European, "5"),
                         new (SizeType.American, "L"),
                     }),
                new (ClothesSizeType.Underwear, 50,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "50"),
                         new (SizeType.European, "6"),
                         new (SizeType.American, "XL"),
                     }),
                new (ClothesSizeType.Underwear, 52,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "50"),
                         new (SizeType.European, "7"),
                         new (SizeType.American, "XXL"),
                     }),
                new (ClothesSizeType.Underwear, 54,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "54"),
                         new (SizeType.European, "8"),
                         new (SizeType.American, "XXXL"),
                     }),
            };

        /// <summary>
        /// Связующие размеры. Носки
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> SocksSizes =>
            new List<SizeGroupDomain>
            {
                 new (ClothesSizeType.Socks, 39,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "39"),
                         new (SizeType.European, "39"),
                         new (SizeType.American, "9.5"),
                     }),
                 new (ClothesSizeType.Socks, 40,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "40"),
                         new (SizeType.European, "40"),
                         new (SizeType.American, "10"),
                     }),
                 new (ClothesSizeType.Socks, 41,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "41"),
                         new (SizeType.European, "41"),
                         new (SizeType.American, "10.5"),
                     }),
                 new (ClothesSizeType.Socks, 42,
                      new List<SizeDomain> {
                          new (SizeType.Russian, "42"),
                          new (SizeType.European, "42"),
                          new (SizeType.American, "11"),
                      }),
                 new (ClothesSizeType.Socks, 43,
                      new List<SizeDomain> {
                          new (SizeType.Russian, "43"),
                          new (SizeType.European, "43"),
                          new (SizeType.American, "11.5"),
                      }),
                 new (ClothesSizeType.Socks, 44,
                      new List<SizeDomain> {
                          new (SizeType.Russian, "44"),
                          new (SizeType.European, "44"),
                          new (SizeType.American, "12"),
                      }),
                 new (ClothesSizeType.Socks, 45,
                      new List<SizeDomain> {
                          new (SizeType.Russian, "45"),
                          new (SizeType.European, "45"),
                          new (SizeType.American, "12.5"),
                      }),
            };

        /// <summary>
        /// Связующие размеры. Обувь
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> ShoesSizes =>
            new List<SizeGroupDomain>
            {
                 new (ClothesSizeType.Shoes, 35,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "35"),
                         new (SizeType.European, "35"),
                         new (SizeType.American, "3"),
                     }),
                 new (ClothesSizeType.Shoes, 36,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "36"),
                         new (SizeType.European, "36"),
                         new (SizeType.American, "4"),
                     }),
                 new (ClothesSizeType.Shoes, 37,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "37"),
                         new (SizeType.European, "37"),
                         new (SizeType.American, "5"),
                     }),
                 new (ClothesSizeType.Shoes, 38,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "38"),
                         new (SizeType.European, "38"),
                         new (SizeType.American, "6"),
                     }),
                 new (ClothesSizeType.Shoes, 39,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "39"),
                         new (SizeType.European, "39"),
                         new (SizeType.American, "7"),
                     }),
                 new (ClothesSizeType.Shoes, 40,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "40"),
                         new (SizeType.European, "41"),
                         new (SizeType.American, "8"),
                     }),
                 new (ClothesSizeType.Shoes, 41,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "41"),
                         new (SizeType.European, "42"),
                         new (SizeType.American, "9"),
                     }),
                 new (ClothesSizeType.Shoes, 42,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "42"),
                         new (SizeType.European, "43"),
                         new (SizeType.American, "10"),
                     }),
                 new (ClothesSizeType.Shoes, 43,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "43"),
                         new (SizeType.European, "44"),
                         new (SizeType.American, "11"),
                     }),
                 new (ClothesSizeType.Shoes, 44,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "44"),
                         new (SizeType.European, "45"),
                         new (SizeType.American, "12"),
                     }),
                 new (ClothesSizeType.Shoes, 45,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "45"),
                         new (SizeType.European, "46"),
                         new (SizeType.American, "12.5"),
                     }),
            };

        /// <summary>
        /// Связующие размеры. Платья
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> DressSizes =>
            new List<SizeGroupDomain>
            {
                new (ClothesSizeType.Dress, 40,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "40"),
                         new (SizeType.European, "34"),
                         new (SizeType.American, "6"),
                     }),
                new (ClothesSizeType.Dress, 42,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "42"),
                         new (SizeType.European, "36"),
                         new (SizeType.American, "8"),
                     }),
                new (ClothesSizeType.Dress, 44,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "44"),
                         new (SizeType.European, "38"),
                         new (SizeType.American, "10"),
                     }),
                 new (ClothesSizeType.Dress, 46,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "46"),
                         new (SizeType.European, "40"),
                         new (SizeType.American, "12"),
                     }),
                 new (ClothesSizeType.Dress, 48,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "48"),
                         new (SizeType.European, "42"),
                         new (SizeType.American, "14"),
                     }),
                 new (ClothesSizeType.Dress, 50,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "50"),
                         new (SizeType.European, "44"),
                         new (SizeType.American, "16"),
                     }),
                 new (ClothesSizeType.Dress, 52,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "52"),
                         new (SizeType.European, "46"),
                         new (SizeType.American, "18"),
                     }),
                 new (ClothesSizeType.Dress, 54,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "54"),
                         new (SizeType.European, "48"),
                         new (SizeType.American, "20"),
                     }),
                 new (ClothesSizeType.Dress, 56,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "56"),
                         new (SizeType.European, "50"),
                         new (SizeType.American, "22"),
                     }),
                 new (ClothesSizeType.Dress, 58,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "58"),
                         new (SizeType.European, "52"),
                         new (SizeType.American, "24"),
                     }),
            };

        /// <summary>
        /// Связующие размеры. Блузки
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupDomain> BlouseSizes =>
            new List<SizeGroupDomain>
            {
                new (ClothesSizeType.Blouse, 40,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "40"),
                         new (SizeType.European, "40"),
                         new (SizeType.American, "32"),
                     }),
                new (ClothesSizeType.Blouse, 42,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "42"),
                         new (SizeType.European, "42"),
                         new (SizeType.American, "34"),
                     }),
                 new (ClothesSizeType.Blouse, 44,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "44"),
                         new (SizeType.European, "44"),
                         new (SizeType.American, "36"),
                     }),
                 new (ClothesSizeType.Blouse, 46,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "46"),
                         new (SizeType.European, "46"),
                         new (SizeType.American, "38"),
                     }),
                 new (ClothesSizeType.Blouse, 48,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "48"),
                         new (SizeType.European, "48"),
                         new (SizeType.American, "40"),
                     }),
                 new (ClothesSizeType.Blouse, 50,
                     new List<SizeDomain> {
                         new (SizeType.Russian, "50"),
                         new (SizeType.European, "50"),
                         new (SizeType.American, "42"),
                     }),
            };
    }
}