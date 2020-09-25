using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

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
                (new ClothesTypeEntity("Пальто"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Куртки"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Толстовки"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Свитера"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Рубашки"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Брюки"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Джинсы"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Футболки"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Обувь"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Шорты"), GenderInitialize.MaleAndFemale),
                (new ClothesTypeEntity("Платья"), new List<GenderEntity> {GenderInitialize.Female}),
                (new ClothesTypeEntity("Юбки"), new List<GenderEntity> {GenderInitialize.Female}),
            }.AsReadOnly();

        /// <summary>
        /// Начальные данные вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeData =>
            ClothesTypeWithGenderData.Select(clothesTypeWithGender => clothesTypeWithGender.ClothesType).ToList().AsReadOnly();
    }
}