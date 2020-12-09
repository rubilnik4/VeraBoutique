using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
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
           : this(name, Enumerable.Empty<ClothesTypeEntity>())
        { }

        public CategoryEntity(string name, IEnumerable<ClothesTypeEntity>? clothesTypes)
            : base(name)
        {
            ClothesTypes = clothesTypes?.ToList();
        }

        /// <summary>
        /// Связующие сущности категории и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeEntity>? ClothesTypes { get; }
    }
}