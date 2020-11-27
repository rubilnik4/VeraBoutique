using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы группы размеров одежды
    /// </summary>
    public static class SizeGroupInitialize
    {
        /// <summary>
        /// Начальные данные таблицы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeGroupData =>
            SizeOutwearData.
            Concat(SizeShirtData).
            Concat(SizeTShirtData).
            Concat(SizeJacketData).
            Concat(SizePantsData).
            Concat(SizeUnderwearData).
            Concat(SizeSocksData).
            Concat(SizeShoesData).
            Concat(SizeDressData).
            Concat(SizeBlouseData).
            ToList().AsReadOnly();

        /// <summary>
        /// Размеры. Верхняя одежда
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeOutwearData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Outerwear, 38),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 40),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 42),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 44),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 46),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 48),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 50),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 52),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 54),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 56),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 58),
                new SizeGroupEntity(ClothesSizeType.Outerwear, 60),
            };

        /// <summary>
        /// Размеры. Рубашки
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeShirtData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Shirt, 44),
                new SizeGroupEntity(ClothesSizeType.Shirt, 48),
                new SizeGroupEntity(ClothesSizeType.Shirt, 50),
                new SizeGroupEntity(ClothesSizeType.Shirt, 54),
                new SizeGroupEntity(ClothesSizeType.Shirt, 56),
                new SizeGroupEntity(ClothesSizeType.Shirt, 58),
            };

        /// <summary>
        /// Размеры. Футболки
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeTShirtData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Tshirt, 42),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 44),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 46),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 48),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 50),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 52),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 54),
                new SizeGroupEntity(ClothesSizeType.Tshirt, 56),
            };

        /// <summary>
        /// Размеры. Куртки
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeJacketData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Jacket, 38),
                new SizeGroupEntity(ClothesSizeType.Jacket, 40),
                new SizeGroupEntity(ClothesSizeType.Jacket, 42),
                new SizeGroupEntity(ClothesSizeType.Jacket, 44),
                new SizeGroupEntity(ClothesSizeType.Jacket, 46),
                new SizeGroupEntity(ClothesSizeType.Jacket, 48),
                new SizeGroupEntity(ClothesSizeType.Jacket, 50),
                new SizeGroupEntity(ClothesSizeType.Jacket, 52),
                new SizeGroupEntity(ClothesSizeType.Jacket, 54),
                new SizeGroupEntity(ClothesSizeType.Jacket, 56),
            };

        /// <summary>
        /// Размеры. Штаны
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizePantsData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Pants, 44),
                new SizeGroupEntity(ClothesSizeType.Pants, 46),
                new SizeGroupEntity(ClothesSizeType.Pants, 48),
                new SizeGroupEntity(ClothesSizeType.Pants, 50),
                new SizeGroupEntity(ClothesSizeType.Pants, 52),
                new SizeGroupEntity(ClothesSizeType.Pants, 54),
                new SizeGroupEntity(ClothesSizeType.Pants, 56),
            };

        /// <summary>
        /// Размеры. Нижнее белье
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeUnderwearData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Underwear, 42),
                new SizeGroupEntity(ClothesSizeType.Underwear, 44),
                new SizeGroupEntity(ClothesSizeType.Underwear, 46),
                new SizeGroupEntity(ClothesSizeType.Underwear, 48),
                new SizeGroupEntity(ClothesSizeType.Underwear, 50),
                new SizeGroupEntity(ClothesSizeType.Underwear, 52),
                new SizeGroupEntity(ClothesSizeType.Underwear, 54),
            };

        /// <summary>
        /// Размеры. Носки
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeSocksData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Socks, 39),
                new SizeGroupEntity(ClothesSizeType.Socks, 40),
                new SizeGroupEntity(ClothesSizeType.Socks, 41),
                new SizeGroupEntity(ClothesSizeType.Socks, 42),
                new SizeGroupEntity(ClothesSizeType.Socks, 43),
                new SizeGroupEntity(ClothesSizeType.Socks, 44),
                new SizeGroupEntity(ClothesSizeType.Socks, 45),
            };

        /// <summary>
        /// Размеры. Обувь
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeShoesData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Shoes, 35),
                new SizeGroupEntity(ClothesSizeType.Shoes, 36),
                new SizeGroupEntity(ClothesSizeType.Shoes, 37),
                new SizeGroupEntity(ClothesSizeType.Shoes, 38),
                new SizeGroupEntity(ClothesSizeType.Shoes, 39),
                new SizeGroupEntity(ClothesSizeType.Shoes, 40),
                new SizeGroupEntity(ClothesSizeType.Shoes, 41),
                new SizeGroupEntity(ClothesSizeType.Shoes, 42),
                new SizeGroupEntity(ClothesSizeType.Shoes, 43),
                new SizeGroupEntity(ClothesSizeType.Shoes, 44),
                new SizeGroupEntity(ClothesSizeType.Shoes, 45),
            };

        /// <summary>
        /// Размеры. Платья
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeDressData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Dress, 40),
                new SizeGroupEntity(ClothesSizeType.Dress, 42),
                new SizeGroupEntity(ClothesSizeType.Dress, 44),
                new SizeGroupEntity(ClothesSizeType.Dress, 46),
                new SizeGroupEntity(ClothesSizeType.Dress, 48),
                new SizeGroupEntity(ClothesSizeType.Dress, 50),
                new SizeGroupEntity(ClothesSizeType.Dress, 52),
                new SizeGroupEntity(ClothesSizeType.Dress, 54),
                new SizeGroupEntity(ClothesSizeType.Dress, 56),
                new SizeGroupEntity(ClothesSizeType.Dress, 58),
            };

        /// <summary>
        /// Размеры. Блузки
        /// </summary>
        public static IReadOnlyCollection<SizeGroupEntity> SizeBlouseData =>
            new List<SizeGroupEntity>
            {
                new SizeGroupEntity(ClothesSizeType.Blouse, 40),
                new SizeGroupEntity(ClothesSizeType.Blouse, 42),
                new SizeGroupEntity(ClothesSizeType.Blouse, 44),
                new SizeGroupEntity(ClothesSizeType.Blouse, 46),
                new SizeGroupEntity(ClothesSizeType.Blouse, 48),
                new SizeGroupEntity(ClothesSizeType.Blouse, 50),
            };
    }
}