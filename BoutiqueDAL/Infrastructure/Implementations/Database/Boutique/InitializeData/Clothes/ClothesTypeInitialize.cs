using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
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
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeData =>
            new List<ClothesTypeEntity>
            {
                ToClothesTypeEntity("Пальто", Outerwear, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Куртки", Outerwear, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Толстовки", Outerwear, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Свитера", Outerwear, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Рубашки", Outerwear, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Футболки", Outerwear, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Брюки", Pants, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Джинсы", Pants, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Шорты", Pants, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Кроссовки", Shoes, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Туфли", Shoes, GenderInitialize.MaleAndFemale),
                ToClothesTypeEntity("Платья", Dress, new List<GenderEntity> {GenderInitialize.Female}),
                ToClothesTypeEntity("Юбки", Dress, new List<GenderEntity> {GenderInitialize.Female}),
            }.AsReadOnly();

        /// <summary>
        /// Связующие сущности типа одежды и пола
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeCategoryData =>
            ClothesTypeData.
            Select(clothesType => new ClothesTypeEntity(clothesType.Name, clothesType.CategoryName)).
            ToList();

        /// <summary>
        /// Связующие сущности типа одежды и пола
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeGenderCompositeEntity> CompositeGenderData =>
            ClothesTypeData.
            SelectMany(clothesType => clothesType.ClothesTypeGenderComposites).
            Distinct().
            ToList();

        /// <summary>
        /// Преобразовать в сущность типа одежды
        /// </summary>
        private static ClothesTypeEntity ToClothesTypeEntity(string clothesTypeName, string categoryName, 
                                                             IEnumerable<GenderEntity> genders) =>
             new ClothesTypeEntity(clothesTypeName, new CategoryEntity(categoryName), ToGenderComposites(genders, clothesTypeName));

        /// <summary>
        /// Связующие сущности типа одежды и пола
        /// </summary>
        private static IEnumerable<ClothesTypeGenderCompositeEntity> ToGenderComposites(IEnumerable<GenderEntity> genders,
                                                                                        string clothesTypeName) =>
            genders.Select(gender => new ClothesTypeGenderCompositeEntity(clothesTypeName, gender.GenderType));
    }
}