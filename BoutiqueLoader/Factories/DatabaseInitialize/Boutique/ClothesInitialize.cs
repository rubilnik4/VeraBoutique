using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;

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
                                                  index * 1000, ImageResource, GenderInitialize.Male,
                                                  ClothesTypeInitialize.TshirtClothesType, ColorInitialize.ColorClothes,
                                                  SizeGroupInitialize.TshirtSizes));

        /// <summary>
        /// Тестовое изображение
        /// </summary>
        private static byte[] ImageResource =>
            Properties.Resources.TShirt;
    }
}