﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoutiqueCommon.Infrastructure.Implementation.Calculate;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueLoader.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы одежды
    /// </summary>
    public static class ClothesInitialize
    {
        /// <summary>
        /// Начальные данные таблицы одежды
        /// </summary>
        public static IReadOnlyCollection<IClothesMainDomain> ClothesMains =>
            ClothesTShirts.
            ToList().AsReadOnly();

        /// <summary>
        /// Вид одежды. 
        /// </summary>
        private static IEnumerable<IClothesMainDomain> ClothesTShirts =>
            Enumerable.Range(1, 25).
            Select(index => new ClothesMainDomain(0, $"Футболка {index}", $"Футболка тестовая шерстяная {index}",
                                                  index * 100, ImageResource, GenderInitialize.Male,
                                                  ClothesTypeInitialize.TshirtClothesType,
                                                  Colors,
                                                  Sizes));

        /// <summary>
        /// Случайные цвета
        /// </summary>
        private static IReadOnlyCollection<IColorDomain> Colors =>
            ColorInitialize.ColorClothes.ToList().
            Map(colors => colors[RandomNumbers.GetRandom(0, colors.Count)]).
            Map(color => new List<IColorDomain>() { color });

        /// <summary>
        /// Случайные цвета
        /// </summary>
        private static IReadOnlyCollection<ISizeGroupMainDomain> Sizes =>
            SizeGroupInitialize.TshirtSizes.ToList().
            Map(sizes => sizes[RandomNumbers.GetRandom(0, sizes.Count)]).
            Map(size => new List<ISizeGroupMainDomain>() { size });

        /// <summary>
        /// Тестовое изображение
        /// </summary>
        private static byte[] ImageResource =>
            Properties.Resources.TShirt;
    }
}