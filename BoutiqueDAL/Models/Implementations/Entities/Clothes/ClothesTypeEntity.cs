using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public class ClothesTypeEntity : ClothesTypeBase, IClothesTypeEntity
    {
        public ClothesTypeEntity(IClothesTypeBase clothesType)
           : this(clothesType.Name, clothesType.SizeTypeDefault, clothesType.CategoryName)
        { }

        public ClothesTypeEntity(string name, SizeType sizeTypeDefault, string categoryName)
           : this(name, sizeTypeDefault, categoryName, null, null)
        { }

        public ClothesTypeEntity(IClothesTypeBase clothesType, string categoryName)
           : this(clothesType.Name, clothesType.SizeTypeDefault, categoryName, null, null)
        { }

        public ClothesTypeEntity(IClothesTypeBase clothesType, CategoryEntity category)
            : this(clothesType.Name, clothesType.SizeTypeDefault, category)
        { }

        public ClothesTypeEntity(string name, SizeType sizeTypeDefault, CategoryEntity category)
           : this(name, sizeTypeDefault, category.Name, category, null)
        { }

        public ClothesTypeEntity(string name, SizeType sizeTypeDefault, string categoryName, CategoryEntity? category, 
                                 IEnumerable<ClothesEntity>? clothes)
           : base(name, sizeTypeDefault, categoryName)
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