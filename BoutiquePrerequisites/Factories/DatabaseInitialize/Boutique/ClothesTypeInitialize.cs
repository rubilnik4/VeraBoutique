using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы, связующей пол и вид одежды
    /// </summary>
    public class ClothesTypeInitialize
    {
        /// <summary>
        /// Начальные данные вида одежды, совмещенные с полом
        /// </summary>
        public static IReadOnlyCollection<IClothesTypeDomain> ClothesTypes =>
            new List<ClothesTypeDomain>
            {
                new("Пальто", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new("Куртки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new("Толстовки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new("Свитера", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new("Рубашки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new("Футболки", CategoryInitialize.Outerwear, GenderInitialize.MaleAndFemale),
                new("Брюки", CategoryInitialize.Pants, GenderInitialize.MaleAndFemale),
                new("Джинсы", CategoryInitialize.Pants, GenderInitialize.MaleAndFemale),
                new("Шорты", CategoryInitialize.Pants, GenderInitialize.MaleAndFemale),
                new("Кроссовки", CategoryInitialize.Shoes, GenderInitialize.MaleAndFemale),
                new("Туфли", CategoryInitialize.Shoes, GenderInitialize.MaleAndFemale),
                new("Платья", CategoryInitialize.Dress, new List<IGenderDomain> {GenderInitialize.Female}),
                new("Юбки", CategoryInitialize.Dress, new List<IGenderDomain> {GenderInitialize.Female}),
            }.AsReadOnly();

        ///// <summary>
        ///// Связующие сущности типа одежды и пола
        ///// </summary>
        //public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeCategoryData =>
        //    ClothesTypeData.
        //    Select(clothesType => new ClothesTypeEntity(clothesType)).
        //    ToList();

        ///// <summary>
        ///// Связующие сущности типа одежды и пола
        ///// </summary>
        //public static IReadOnlyCollection<ClothesTypeGenderCompositeEntity> CompositeGenderData =>
        //    ClothesTypeData.
        //    SelectMany(clothesType => clothesType.ClothesTypeGenderComposites!).
        //    Distinct().
        //    ToList();

        ///// <summary>
        ///// Преобразовать в сущность типа одежды
        ///// </summary>
        //private static ClothesTypeEntity ToClothesTypeEntity(string clothesTypeName, string categoryName, 
        //                                                     IEnumerable<GenderEntity> genders) =>
        //     new (clothesTypeName, new CategoryEntity(categoryName), ToGenderComposites(genders, clothesTypeName));

        ///// <summary>
        ///// Связующие сущности типа одежды и пола
        ///// </summary>
        //private static IEnumerable<ClothesTypeGenderCompositeEntity> ToGenderComposites(IEnumerable<GenderEntity> genders,
        //                                                                                string clothesTypeName) =>
        //    genders.Select(gender => new ClothesTypeGenderCompositeEntity(clothesTypeName, gender.GenderType));
    }
}