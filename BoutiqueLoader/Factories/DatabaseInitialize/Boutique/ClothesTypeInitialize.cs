using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;

namespace BoutiqueLoader.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы вида одежды
    /// </summary>
    public class ClothesTypeInitialize
    {
        /// <summary>
        /// Начальные данные вида одежды
        /// </summary>
        public static IReadOnlyCollection<IClothesTypeMainDomain> ClothesTypeMains =>
            new List<IClothesTypeMainDomain>
            {
                new ClothesTypeMainDomain("Пальто", CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Куртки", CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Толстовки", CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Свитера", CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Рубашки", CategoryInitialize.Outerwear),
                TshirtClothesType,
                new ClothesTypeMainDomain("Брюки", CategoryInitialize.Pants),
                new ClothesTypeMainDomain("Джинсы", CategoryInitialize.Pants),
                new ClothesTypeMainDomain("Шорты", CategoryInitialize.Pants),
                new ClothesTypeMainDomain("Кроссовки", CategoryInitialize.Shoes),
                new ClothesTypeMainDomain("Туфли", CategoryInitialize.Shoes),
                new ClothesTypeMainDomain("Платья", CategoryInitialize.Dress),
                new ClothesTypeMainDomain("Юбки", CategoryInitialize.Dress),
                new ClothesTypeMainDomain("Бижутерия", CategoryInitialize.Accessories),
            }.AsReadOnly();

        /// <summary>
        /// Футболки
        /// </summary>
        public static IClothesTypeMainDomain TshirtClothesType =>
            new ClothesTypeMainDomain("Футболки", CategoryInitialize.Outerwear);
    }
}