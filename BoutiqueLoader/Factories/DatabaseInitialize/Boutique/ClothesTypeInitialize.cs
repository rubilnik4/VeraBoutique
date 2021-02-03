using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

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
        public static IReadOnlyCollection<IClothesTypeDomain> ClothesTypes =>
            new List<IClothesTypeDomain>
            {
                new ClothesTypeDomain("Пальто", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Куртки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Толстовки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Свитера", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Рубашки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                Tshort,
                new ClothesTypeDomain("Брюки", CategoryInitialize.Pants, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Джинсы", CategoryInitialize.Pants, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Шорты", CategoryInitialize.Pants, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Кроссовки", CategoryInitialize.Shoes, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Туфли", CategoryInitialize.Shoes, GenderInitialize.MaleAndFemale),
                new ClothesTypeDomain("Платья", CategoryInitialize.Dress, new List<IGenderDomain> {GenderInitialize.Female}),
                new ClothesTypeDomain("Юбки", CategoryInitialize.Dress, new List<IGenderDomain> {GenderInitialize.Female}),
            }.AsReadOnly();

        /// <summary>
        /// Вид одежды. Футболка
        /// </summary>
        public static IClothesTypeDomain Tshort =>
            new ClothesTypeDomain("Футболки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale);
    }
}