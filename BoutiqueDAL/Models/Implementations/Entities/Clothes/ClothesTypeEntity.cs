using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public class ClothesTypeEntity : ClothesTypeBase, IClothesTypeEntity
    {
        public ClothesTypeEntity(IClothesTypeBase clothesType)
           : this(clothesType.Name, clothesType.CategoryName)
        { }

        public ClothesTypeEntity(string name, string categoryName)
           : this(name, categoryName, null, null)
        { }

        public ClothesTypeEntity(IClothesTypeBase clothesType, string categoryName)
           : this(clothesType.Name, categoryName, null, null)
        { }

        public ClothesTypeEntity(IClothesTypeBase clothesType, CategoryEntity category)
            : this(clothesType.Name, category)
        { }

        public ClothesTypeEntity(string name, CategoryEntity category)
           : this(name, category.Name, category, null)
        { }

        public ClothesTypeEntity(string name, string categoryName, CategoryEntity? category, 
                                 IEnumerable<ClothesEntity>? clothes)
           : base(name, categoryName)
        {
            Category = category;
            Clothes = clothes?.ToList();
        }

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