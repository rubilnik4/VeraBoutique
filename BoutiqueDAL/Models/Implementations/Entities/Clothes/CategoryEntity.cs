using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Категория одежды. Сущность базы данных
    /// </summary>
    public class CategoryEntity : CategoryBase, ICategoryEntity
    {
        public CategoryEntity(ICategoryBase category)
           : this(category.Name)
        { }

        public CategoryEntity(string name)
           : this(name, null, null)
        { }

        public CategoryEntity(ICategoryBase category, IEnumerable<GenderCategoryCompositeEntity>? genderCategoryComposites)
         : this(category.Name, genderCategoryComposites)
        { }

        public CategoryEntity(string name, IEnumerable<GenderCategoryCompositeEntity>? genderCategoryComposites)
          : this(name, genderCategoryComposites, null)
        { }

        public CategoryEntity(string name, IEnumerable<ClothesTypeEntity>? clothesTypes)
         : this(name, null, clothesTypes)
        { }

        public CategoryEntity(string name, IEnumerable<GenderCategoryCompositeEntity>? genderCategoryComposites,
                              IEnumerable<ClothesTypeEntity>? clothesTypes)
            : base(name)
        {
            GenderCategoryComposites = genderCategoryComposites?.ToList();
            ClothesTypes = clothesTypes?.ToList();
        }

        /// <summary>
        /// Связующие сущности типа пола и категории одежды
        /// </summary>
        public IReadOnlyCollection<GenderCategoryCompositeEntity>? GenderCategoryComposites { get; }

        /// <summary>
        /// Связующие сущности категории и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeEntity>? ClothesTypes { get; }
    }
}