using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using static BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes.CategoryInitialize;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы, связующей пол и вид одежды
    /// </summary>
    public class ClothesTypeInitialize
    {
        /// <summary>
        /// Начальные данные вида одежды, совмещенные с полом
        /// </summary>
        public static IReadOnlyCollection<(ClothesTypeEntity ClothesType, IReadOnlyCollection<GenderEntity> Genders)> ClothesTypeWithGenderData =>
            new List<(ClothesTypeEntity ClothesType, IReadOnlyCollection<GenderEntity> Genders)>
            {
                (new ClothesTypeEntity("Пальто", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Куртки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Толстовки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Свитера", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Рубашки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Футболки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Брюки", Pants), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Джинсы", Pants), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Шорты", Pants), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Кроссовки", Shoes), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Туфли", Shoes), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Платья", Dress), new List<GenderEntity> {GenderInitialize.Female}),
                (new ClothesTypeEntity("Юбки", Dress), new List<GenderEntity> {GenderInitialize.Female}),
            }.AsReadOnly();

        /// <summary>
        /// Начальные данные вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeData =>
            ClothesTypeWithGenderData.Select(clothesTypeWithGender => clothesTypeWithGender.ClothesType).ToList().AsReadOnly();
    }
}