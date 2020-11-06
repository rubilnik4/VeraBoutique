using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая сущность базы данных
    /// </summary>
    public class ClothesTypeShortEntity : ClothesType, IClothesTypeShortEntity
    {
        public ClothesTypeShortEntity(string name, string categoryName)
          : this(name, categoryName, null, Enumerable.Empty<ClothesEntity>())
        { }

        public ClothesTypeShortEntity(string name, string categoryName, CategoryEntity? category,
                                      IEnumerable<ClothesEntity>? clothes)
           : base(name)
        {
            CategoryName = categoryName;
            Category = category;
            Clothes = clothes?.ToList();
        }

        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public CategoryEntity? Category { get; }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}