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
            new List<ClothesMainDomain>
            {
                new (0,"Футболка 1","Футболка тестовая 1", 1000, ImageResource, GenderInitialize.Male,
                     ClothesTypeInitialize.TshirtClothesType, ColorInitialize.ColorClothes, SizeGroupInitialize.TshirtSizes),
                new (0,"Футболка 2","Футболка тестовая 2", 2000, ImageResource, GenderInitialize.Male,
                     ClothesTypeInitialize.TshirtClothesType, ColorInitialize.ColorClothes, SizeGroupInitialize.TshirtSizes),
                new (0,"Футболка 3","Футболка тестовая 3", 3000, ImageResource, GenderInitialize.Male,
                     ClothesTypeInitialize.TshirtClothesType, ColorInitialize.ColorClothes, SizeGroupInitialize.TshirtSizes),
                new (0,"Футболка 4","Футболка тестовая 4", 4000, ImageResource, GenderInitialize.Male,
                     ClothesTypeInitialize.TshirtClothesType, ColorInitialize.ColorClothes, SizeGroupInitialize.TshirtSizes)
            };

        /// <summary>
        /// Тестовое изображение
        /// </summary>
        private static byte[] ImageResource =>
            Properties.Resources.TShirt;
    }
}