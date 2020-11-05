using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
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
        public static IReadOnlyCollection<(ClothesTypeFullEntity ClothesType, IReadOnlyCollection<GenderEntity> Genders)> ClothesTypeWithGenderData =>
            new List<(ClothesTypeFullEntity ClothesType, IReadOnlyCollection<GenderEntity> Genders)>
            {
                (new ClothesTypeFullEntity("Пальто", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Куртки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Толстовки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Свитера", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Рубашки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Футболки", Outerwear), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Брюки", Pants), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Джинсы", Pants), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Шорты", Pants), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Кроссовки", Shoes), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Туфли", Shoes), GenderInitialize.MaleAndFemale),
                (new ClothesTypeFullEntity("Платья", Dress), new List<GenderEntity> {GenderInitialize.Female}),
                (new ClothesTypeFullEntity("Юбки", Dress), new List<GenderEntity> {GenderInitialize.Female}),
            }.AsReadOnly();

        /// <summary>
        /// Начальные данные вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeFullEntity> ClothesTypeData =>
            ClothesTypeWithGenderData.Select(clothesTypeWithGender => clothesTypeWithGender.ClothesType).ToList().AsReadOnly();
    }
}