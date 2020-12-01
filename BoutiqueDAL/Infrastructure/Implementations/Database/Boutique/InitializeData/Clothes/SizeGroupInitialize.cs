using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы группы размеров одежды
    /// </summary>
    public static class SizeGroupInitialize
    {
        /// <summary>
        /// Начальные данные связующей таблицы группы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<SizeGroupCompositeEntity> CompositeSizeData =>
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
        private static IReadOnlyCollection<SizeGroupCompositeEntity> OutwearSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "38", ClothesSizeType.Outerwear, 38),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Outerwear, 38),
                new SizeGroupCompositeEntity(SizeType.American,"28", ClothesSizeType.Outerwear, 38),

                new SizeGroupCompositeEntity(SizeType.Russian, "40", ClothesSizeType.Outerwear, 40),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Outerwear, 40),
                new SizeGroupCompositeEntity(SizeType.American,"30", ClothesSizeType.Outerwear, 40),

                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Outerwear, 42),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Outerwear, 42),
                new SizeGroupCompositeEntity(SizeType.American,"32", ClothesSizeType.Outerwear, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Outerwear, 44),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Outerwear, 44),
                new SizeGroupCompositeEntity(SizeType.American,"34", ClothesSizeType.Outerwear, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Outerwear, 46),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Outerwear, 46),
                new SizeGroupCompositeEntity(SizeType.American,"36", ClothesSizeType.Outerwear, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Outerwear, 48),
                new SizeGroupCompositeEntity(SizeType.European,"48", ClothesSizeType.Outerwear, 48),
                new SizeGroupCompositeEntity(SizeType.American,"38", ClothesSizeType.Outerwear, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Outerwear, 50),
                new SizeGroupCompositeEntity(SizeType.European,"50", ClothesSizeType.Outerwear, 50),
                new SizeGroupCompositeEntity(SizeType.American,"40", ClothesSizeType.Outerwear, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "52", ClothesSizeType.Outerwear, 52),
                new SizeGroupCompositeEntity(SizeType.European,"52", ClothesSizeType.Outerwear, 52),
                new SizeGroupCompositeEntity(SizeType.American,"42", ClothesSizeType.Outerwear, 52),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Outerwear, 54),
                new SizeGroupCompositeEntity(SizeType.European,"54", ClothesSizeType.Outerwear, 54),
                new SizeGroupCompositeEntity(SizeType.American,"44", ClothesSizeType.Outerwear, 54),

                new SizeGroupCompositeEntity(SizeType.Russian, "56", ClothesSizeType.Outerwear, 56),
                new SizeGroupCompositeEntity(SizeType.European,"56", ClothesSizeType.Outerwear, 56),
                new SizeGroupCompositeEntity(SizeType.American,"46", ClothesSizeType.Outerwear, 56),

                new SizeGroupCompositeEntity(SizeType.Russian, "58", ClothesSizeType.Outerwear, 58),
                new SizeGroupCompositeEntity(SizeType.European,"58", ClothesSizeType.Outerwear, 58),
                new SizeGroupCompositeEntity(SizeType.American,"48", ClothesSizeType.Outerwear, 58),

                new SizeGroupCompositeEntity(SizeType.Russian, "60", ClothesSizeType.Outerwear, 60),
                new SizeGroupCompositeEntity(SizeType.European,"60", ClothesSizeType.Outerwear, 60),
                new SizeGroupCompositeEntity(SizeType.American,"50", ClothesSizeType.Outerwear, 60),
            };

        /// <summary>
        /// Начальные данные таблицы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeGroupData =>
            CompositeSizeData.
            GroupBy(sizeComposite => new { sizeComposite.ClothesSizeType, sizeComposite.SizeNormalize }).
            Select(sizeCompositeGroup => new SizeGroupEntity(sizeCompositeGroup.First().ClothesSizeType,
                                                             sizeCompositeGroup.First().SizeNormalize)).
            ToList().AsReadOnly();

        /// <summary>
        /// Связующие размеры. Рубашки
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> ShirtSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Shirt, 44),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Shirt, 44),
                new SizeGroupCompositeEntity(SizeType.American,"S", ClothesSizeType.Shirt, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Shirt, 48),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Shirt, 48),
                new SizeGroupCompositeEntity(SizeType.American,"M", ClothesSizeType.Shirt, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Shirt, 50),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Shirt, 50),
                new SizeGroupCompositeEntity(SizeType.American,"L", ClothesSizeType.Shirt, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Shirt, 54),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Shirt, 54),
                new SizeGroupCompositeEntity(SizeType.American,"XL", ClothesSizeType.Shirt, 54),

                new SizeGroupCompositeEntity(SizeType.Russian, "56", ClothesSizeType.Shirt, 56),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Shirt, 56),
                new SizeGroupCompositeEntity(SizeType.American,"XXL", ClothesSizeType.Shirt, 56),

                new SizeGroupCompositeEntity(SizeType.Russian, "58", ClothesSizeType.Shirt, 58),
                new SizeGroupCompositeEntity(SizeType.European,"48", ClothesSizeType.Shirt, 58),
                new SizeGroupCompositeEntity(SizeType.American,"XXXL", ClothesSizeType.Shirt, 58),
            };

        /// <summary>
        /// Связующие размеры. Футболки
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> TshirtSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Tshirt, 42),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Tshirt, 42),
                new SizeGroupCompositeEntity(SizeType.American,"XXS", ClothesSizeType.Tshirt, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Tshirt, 44),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Tshirt, 44),
                new SizeGroupCompositeEntity(SizeType.American,"XS", ClothesSizeType.Tshirt, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Tshirt, 46),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Tshirt, 46),
                new SizeGroupCompositeEntity(SizeType.American,"S", ClothesSizeType.Tshirt, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Tshirt, 48),
                new SizeGroupCompositeEntity(SizeType.European,"50", ClothesSizeType.Tshirt, 48),
                new SizeGroupCompositeEntity(SizeType.American,"M", ClothesSizeType.Tshirt, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Tshirt, 50),
                new SizeGroupCompositeEntity(SizeType.European,"54", ClothesSizeType.Tshirt, 50),
                new SizeGroupCompositeEntity(SizeType.American,"L", ClothesSizeType.Tshirt, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "52", ClothesSizeType.Tshirt, 52),
                new SizeGroupCompositeEntity(SizeType.European,"58", ClothesSizeType.Tshirt, 52),
                new SizeGroupCompositeEntity(SizeType.American,"XL", ClothesSizeType.Tshirt, 52),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Tshirt, 54),
                new SizeGroupCompositeEntity(SizeType.European,"60", ClothesSizeType.Tshirt, 54),
                new SizeGroupCompositeEntity(SizeType.American,"XXL", ClothesSizeType.Tshirt, 54),

                new SizeGroupCompositeEntity(SizeType.Russian, "56", ClothesSizeType.Tshirt, 56),
                new SizeGroupCompositeEntity(SizeType.European,"62", ClothesSizeType.Tshirt, 56),
                new SizeGroupCompositeEntity(SizeType.American,"XXXL", ClothesSizeType.Tshirt, 56),
            };

        /// <summary>
        /// Связующие размеры. Куртки
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> JacketSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "38", ClothesSizeType.Jacket, 38),
                new SizeGroupCompositeEntity(SizeType.European,"32", ClothesSizeType.Jacket, 38),
                new SizeGroupCompositeEntity(SizeType.American,"4", ClothesSizeType.Jacket, 38),

                new SizeGroupCompositeEntity(SizeType.Russian, "40", ClothesSizeType.Jacket, 40),
                new SizeGroupCompositeEntity(SizeType.European,"34", ClothesSizeType.Jacket, 40),
                new SizeGroupCompositeEntity(SizeType.American,"6", ClothesSizeType.Jacket, 40),

                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Jacket, 42),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Jacket, 42),
                new SizeGroupCompositeEntity(SizeType.American,"8", ClothesSizeType.Jacket, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Jacket, 44),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Jacket, 44),
                new SizeGroupCompositeEntity(SizeType.American,"10", ClothesSizeType.Jacket, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Jacket, 46),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Jacket, 46),
                new SizeGroupCompositeEntity(SizeType.American,"12", ClothesSizeType.Jacket, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Jacket, 48),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Jacket, 48),
                new SizeGroupCompositeEntity(SizeType.American,"14", ClothesSizeType.Jacket, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Jacket, 50),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Jacket, 50),
                new SizeGroupCompositeEntity(SizeType.American,"16", ClothesSizeType.Jacket, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "52", ClothesSizeType.Jacket, 52),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Jacket, 52),
                new SizeGroupCompositeEntity(SizeType.American,"18", ClothesSizeType.Jacket, 52),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Jacket, 54),
                new SizeGroupCompositeEntity(SizeType.European,"48", ClothesSizeType.Jacket, 54),
                new SizeGroupCompositeEntity(SizeType.American,"20", ClothesSizeType.Jacket, 54),

                new SizeGroupCompositeEntity(SizeType.Russian, "56", ClothesSizeType.Jacket, 56),
                new SizeGroupCompositeEntity(SizeType.European,"50", ClothesSizeType.Jacket, 56),
                new SizeGroupCompositeEntity(SizeType.American,"22", ClothesSizeType.Jacket, 56),
            };

        /// <summary>
        /// Связующие размеры. Штаны
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> PantsSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Pants, 44),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Pants, 44),
                new SizeGroupCompositeEntity(SizeType.American,"XXS", ClothesSizeType.Pants, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Pants, 46),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Pants, 46),
                new SizeGroupCompositeEntity(SizeType.American,"XS", ClothesSizeType.Pants, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Pants, 48),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Pants, 48),
                new SizeGroupCompositeEntity(SizeType.American,"S", ClothesSizeType.Pants, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Pants, 50),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Pants, 50),
                new SizeGroupCompositeEntity(SizeType.American,"M", ClothesSizeType.Pants, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "52", ClothesSizeType.Pants, 52),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Pants, 52),
                new SizeGroupCompositeEntity(SizeType.American,"L", ClothesSizeType.Pants, 52),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Pants, 54),
                new SizeGroupCompositeEntity(SizeType.European,"48", ClothesSizeType.Pants, 54),
                new SizeGroupCompositeEntity(SizeType.American,"XL", ClothesSizeType.Pants, 54),

                new SizeGroupCompositeEntity(SizeType.Russian, "56", ClothesSizeType.Pants, 56),
                new SizeGroupCompositeEntity(SizeType.European,"50", ClothesSizeType.Pants, 56),
                new SizeGroupCompositeEntity(SizeType.American,"XXL", ClothesSizeType.Pants, 56),
            };

        /// <summary>
        /// Связующие размеры. Нижнее белье
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> UnderwearSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Underwear, 42),
                new SizeGroupCompositeEntity(SizeType.European,"2", ClothesSizeType.Underwear, 42),
                new SizeGroupCompositeEntity(SizeType.American,"XS", ClothesSizeType.Underwear, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Underwear, 44),
                new SizeGroupCompositeEntity(SizeType.European,"3", ClothesSizeType.Underwear, 44),
                new SizeGroupCompositeEntity(SizeType.American,"S", ClothesSizeType.Underwear, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Underwear, 46),
                new SizeGroupCompositeEntity(SizeType.European,"4", ClothesSizeType.Underwear, 46),
                new SizeGroupCompositeEntity(SizeType.American,"M", ClothesSizeType.Underwear, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Underwear, 48),
                new SizeGroupCompositeEntity(SizeType.European,"5", ClothesSizeType.Underwear, 48),
                new SizeGroupCompositeEntity(SizeType.American,"L", ClothesSizeType.Underwear, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Underwear, 50),
                new SizeGroupCompositeEntity(SizeType.European,"6", ClothesSizeType.Underwear, 50),
                new SizeGroupCompositeEntity(SizeType.American,"XL", ClothesSizeType.Underwear, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "52", ClothesSizeType.Underwear, 52),
                new SizeGroupCompositeEntity(SizeType.European,"7", ClothesSizeType.Underwear, 52),
                new SizeGroupCompositeEntity(SizeType.American,"XXL", ClothesSizeType.Underwear, 52),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Underwear, 54),
                new SizeGroupCompositeEntity(SizeType.European,"8", ClothesSizeType.Underwear, 54),
                new SizeGroupCompositeEntity(SizeType.American,"XXXL", ClothesSizeType.Underwear, 54),
            };

        /// <summary>
        /// Связующие размеры. Носки
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> SocksSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "39", ClothesSizeType.Socks, 39),
                new SizeGroupCompositeEntity(SizeType.European,"39", ClothesSizeType.Socks, 39),
                new SizeGroupCompositeEntity(SizeType.American,"9.5", ClothesSizeType.Socks, 39),

                new SizeGroupCompositeEntity(SizeType.Russian, "40", ClothesSizeType.Socks, 40),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Socks, 40),
                new SizeGroupCompositeEntity(SizeType.American,"10", ClothesSizeType.Socks, 40),

                new SizeGroupCompositeEntity(SizeType.Russian, "41", ClothesSizeType.Socks, 41),
                new SizeGroupCompositeEntity(SizeType.European,"41", ClothesSizeType.Socks, 41),
                new SizeGroupCompositeEntity(SizeType.American,"10.5", ClothesSizeType.Socks, 41),

                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Socks, 42),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Socks, 42),
                new SizeGroupCompositeEntity(SizeType.American,"11", ClothesSizeType.Socks, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "43", ClothesSizeType.Socks, 43),
                new SizeGroupCompositeEntity(SizeType.European,"43", ClothesSizeType.Socks, 43),
                new SizeGroupCompositeEntity(SizeType.American,"11.5", ClothesSizeType.Socks, 43),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Socks, 44),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Socks, 44),
                new SizeGroupCompositeEntity(SizeType.American,"12", ClothesSizeType.Socks, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "45", ClothesSizeType.Socks, 45),
                new SizeGroupCompositeEntity(SizeType.European,"45", ClothesSizeType.Socks, 45),
                new SizeGroupCompositeEntity(SizeType.American,"12.5", ClothesSizeType.Socks, 45),
            };

        /// <summary>
        /// Связующие размеры. Обувь
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> ShoesSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "35", ClothesSizeType.Shoes, 35),
                new SizeGroupCompositeEntity(SizeType.European,"35", ClothesSizeType.Shoes, 35),
                new SizeGroupCompositeEntity(SizeType.American,"3", ClothesSizeType.Shoes, 35),

                new SizeGroupCompositeEntity(SizeType.Russian, "36", ClothesSizeType.Shoes, 36),
                new SizeGroupCompositeEntity(SizeType.European,"36", ClothesSizeType.Shoes, 36),
                new SizeGroupCompositeEntity(SizeType.American,"4", ClothesSizeType.Shoes, 36),

                new SizeGroupCompositeEntity(SizeType.Russian, "37", ClothesSizeType.Shoes, 37),
                new SizeGroupCompositeEntity(SizeType.European,"37", ClothesSizeType.Shoes, 37),
                new SizeGroupCompositeEntity(SizeType.American,"5", ClothesSizeType.Shoes, 37),

                new SizeGroupCompositeEntity(SizeType.Russian, "38", ClothesSizeType.Shoes, 38),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Shoes, 38),
                new SizeGroupCompositeEntity(SizeType.American,"6", ClothesSizeType.Shoes, 38),

                new SizeGroupCompositeEntity(SizeType.Russian, "39", ClothesSizeType.Shoes, 39),
                new SizeGroupCompositeEntity(SizeType.European,"39", ClothesSizeType.Shoes, 39),
                new SizeGroupCompositeEntity(SizeType.American,"7", ClothesSizeType.Shoes, 39),

                new SizeGroupCompositeEntity(SizeType.Russian, "40", ClothesSizeType.Shoes, 40),
                new SizeGroupCompositeEntity(SizeType.European,"41", ClothesSizeType.Shoes, 40),
                new SizeGroupCompositeEntity(SizeType.American,"8", ClothesSizeType.Shoes, 40),

                new SizeGroupCompositeEntity(SizeType.Russian, "41", ClothesSizeType.Shoes, 41),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Shoes, 41),
                new SizeGroupCompositeEntity(SizeType.American,"9", ClothesSizeType.Shoes, 41),

                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Shoes, 42),
                new SizeGroupCompositeEntity(SizeType.European,"43", ClothesSizeType.Shoes, 42),
                new SizeGroupCompositeEntity(SizeType.American,"10", ClothesSizeType.Shoes, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "43", ClothesSizeType.Shoes, 43),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Shoes, 43),
                new SizeGroupCompositeEntity(SizeType.American,"11", ClothesSizeType.Shoes, 43),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Shoes, 44),
                new SizeGroupCompositeEntity(SizeType.European,"45", ClothesSizeType.Shoes, 44),
                new SizeGroupCompositeEntity(SizeType.American,"12", ClothesSizeType.Shoes, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "45", ClothesSizeType.Shoes, 45),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Shoes, 45),
                new SizeGroupCompositeEntity(SizeType.American,"12.5", ClothesSizeType.Shoes, 45),
            };

        /// <summary>
        /// Связующие размеры. Платья
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> DressSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "40", ClothesSizeType.Dress, 40),
                new SizeGroupCompositeEntity(SizeType.European,"34", ClothesSizeType.Dress, 40),
                new SizeGroupCompositeEntity(SizeType.American,"6", ClothesSizeType.Dress, 40),

                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Dress, 42),
                new SizeGroupCompositeEntity(SizeType.European,"36", ClothesSizeType.Dress, 42),
                new SizeGroupCompositeEntity(SizeType.American,"8", ClothesSizeType.Dress, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Dress, 44),
                new SizeGroupCompositeEntity(SizeType.European,"38", ClothesSizeType.Dress, 44),
                new SizeGroupCompositeEntity(SizeType.American,"10", ClothesSizeType.Dress, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Dress, 46),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Dress, 46),
                new SizeGroupCompositeEntity(SizeType.American,"12", ClothesSizeType.Dress, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Dress, 48),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Dress, 48),
                new SizeGroupCompositeEntity(SizeType.American,"14", ClothesSizeType.Dress, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Dress, 50),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Dress, 50),
                new SizeGroupCompositeEntity(SizeType.American,"16", ClothesSizeType.Dress, 50),

                new SizeGroupCompositeEntity(SizeType.Russian, "52", ClothesSizeType.Dress, 52),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Dress, 52),
                new SizeGroupCompositeEntity(SizeType.American,"18", ClothesSizeType.Dress, 52),

                new SizeGroupCompositeEntity(SizeType.Russian, "54", ClothesSizeType.Dress, 54),
                new SizeGroupCompositeEntity(SizeType.European,"48", ClothesSizeType.Dress, 54),
                new SizeGroupCompositeEntity(SizeType.American,"20", ClothesSizeType.Dress, 54),

                new SizeGroupCompositeEntity(SizeType.Russian, "56", ClothesSizeType.Dress, 56),
                new SizeGroupCompositeEntity(SizeType.European,"50", ClothesSizeType.Dress, 56),
                new SizeGroupCompositeEntity(SizeType.American,"22", ClothesSizeType.Dress, 56),

                new SizeGroupCompositeEntity(SizeType.Russian, "58", ClothesSizeType.Dress, 58),
                new SizeGroupCompositeEntity(SizeType.European,"52", ClothesSizeType.Dress, 58),
                new SizeGroupCompositeEntity(SizeType.American,"24", ClothesSizeType.Dress, 58),
            };

        /// <summary>
        /// Связующие размеры. Блузки
        /// </summary>
        private static IReadOnlyCollection<SizeGroupCompositeEntity> BlouseSizes =>
            new List<SizeGroupCompositeEntity>()
            {
                new SizeGroupCompositeEntity(SizeType.Russian, "40", ClothesSizeType.Blouse, 40),
                new SizeGroupCompositeEntity(SizeType.European,"40", ClothesSizeType.Blouse, 40),
                new SizeGroupCompositeEntity(SizeType.American,"32", ClothesSizeType.Blouse, 40),

                new SizeGroupCompositeEntity(SizeType.Russian, "42", ClothesSizeType.Blouse, 42),
                new SizeGroupCompositeEntity(SizeType.European,"42", ClothesSizeType.Blouse, 42),
                new SizeGroupCompositeEntity(SizeType.American,"34", ClothesSizeType.Blouse, 42),

                new SizeGroupCompositeEntity(SizeType.Russian, "44", ClothesSizeType.Blouse, 44),
                new SizeGroupCompositeEntity(SizeType.European,"44", ClothesSizeType.Blouse, 44),
                new SizeGroupCompositeEntity(SizeType.American,"36", ClothesSizeType.Blouse, 44),

                new SizeGroupCompositeEntity(SizeType.Russian, "46", ClothesSizeType.Blouse, 46),
                new SizeGroupCompositeEntity(SizeType.European,"46", ClothesSizeType.Blouse, 46),
                new SizeGroupCompositeEntity(SizeType.American,"38", ClothesSizeType.Blouse, 46),

                new SizeGroupCompositeEntity(SizeType.Russian, "48", ClothesSizeType.Blouse, 48),
                new SizeGroupCompositeEntity(SizeType.European,"48", ClothesSizeType.Blouse, 48),
                new SizeGroupCompositeEntity(SizeType.American,"40", ClothesSizeType.Blouse, 48),

                new SizeGroupCompositeEntity(SizeType.Russian, "50", ClothesSizeType.Blouse, 50),
                new SizeGroupCompositeEntity(SizeType.European,"50", ClothesSizeType.Blouse, 50),
                new SizeGroupCompositeEntity(SizeType.American,"42", ClothesSizeType.Blouse, 50),
            };
    }
}