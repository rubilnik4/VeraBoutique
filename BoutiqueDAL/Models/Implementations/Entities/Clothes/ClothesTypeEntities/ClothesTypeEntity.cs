using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public class ClothesTypeEntity : ClothesTypeShortEntity, IClothesTypeEntity
    {
        public ClothesTypeEntity(IClothesTypeShortBase clothesTypeShort)
           : this(clothesTypeShort.Name, clothesTypeShort.CategoryName)
        { }

        public ClothesTypeEntity(string name, string categoryName)
           : this(name, categoryName, null, null, null)
        { }

        public ClothesTypeEntity(IClothesTypeShortBase clothesTypeShort, CategoryEntity category,
                                 IEnumerable<ClothesTypeGenderCompositeEntity> clothesTypeGenderComposites)
        : this(clothesTypeShort.Name, category, clothesTypeGenderComposites)
        { }

        public ClothesTypeEntity(string name, CategoryEntity category,
                                IEnumerable<ClothesTypeGenderCompositeEntity> clothesTypeGenderComposites)
           : this(name, category.Name, category, clothesTypeGenderComposites, null)
        { }

        public ClothesTypeEntity(string name, 
                                 string categoryName, CategoryEntity? category,
                                 IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites,
                                 IEnumerable<ClothesEntity>? clothes)
           : base(name, categoryName, clothes)
        {
            Category = category;
            ClothesTypeGenderComposites = clothesTypeGenderComposites?.ToList();
        }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public CategoryEntity? Category { get; }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }

    }
}