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
                new (0,"Футболка","Футболка тестовая", 1500, Encoding.UTF8.GetBytes("test"), GenderInitialize.Male,
                     ClothesTypeInitialize.ClothesTypeMains.First(clothesType => clothesType.Name == "Футболки"), 
                     ColorInitialize.ColorClothes, SizeGroupInitialize.TshirtSizes)
            };
    }
}