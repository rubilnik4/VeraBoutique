using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

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
                new ClothesTypeMainDomain("Пальто", SizeType.Russian, CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Куртки", SizeType.Russian, CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Толстовки", SizeType.American, CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Свитера", SizeType.American, CategoryInitialize.Outerwear),
                new ClothesTypeMainDomain("Рубашки", SizeType.American, CategoryInitialize.Outerwear),
                TshirtClothesType,
                new ClothesTypeMainDomain("Брюки", SizeType.Russian, CategoryInitialize.Pants),
                new ClothesTypeMainDomain("Джинсы", SizeType.Russian, CategoryInitialize.Pants),
                new ClothesTypeMainDomain("Шорты", SizeType.Russian, CategoryInitialize.Pants),
                new ClothesTypeMainDomain("Кроссовки", SizeType.Russian, CategoryInitialize.Shoes),
                new ClothesTypeMainDomain("Туфли", SizeType.Russian, CategoryInitialize.Shoes),
                new ClothesTypeMainDomain("Платья",SizeType.Russian , CategoryInitialize.Dress),
                new ClothesTypeMainDomain("Юбки", SizeType.Russian, CategoryInitialize.Dress),
                new ClothesTypeMainDomain("Бижутерия", SizeType.Default,  CategoryInitialize.Accessories),
            }.AsReadOnly();

        /// <summary>
        /// Футболки
        /// </summary>
        public static IClothesTypeMainDomain TshirtClothesType =>
            new ClothesTypeMainDomain("Футболки", SizeType.American, CategoryInitialize.Outerwear);
    }
}