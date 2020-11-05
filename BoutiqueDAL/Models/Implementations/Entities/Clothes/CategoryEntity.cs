using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Категория одежды. Сущность базы данных
    /// </summary>
    public class CategoryEntity : Category, ICategoryEntity
    {
        public CategoryEntity(string name)
            : this(name, Enumerable.Empty<ClothesTypeFullEntity>())
        { }

        public CategoryEntity(string name, IEnumerable<ClothesTypeFullEntity>? clothesTypeEntities)
            : base(name)
        {
            ClothesTypes = clothesTypeEntities?.ToList();
        }

        /// <summary>
        /// Связующие сущности категории и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeFullEntity>? ClothesTypes { get; }
    }
}