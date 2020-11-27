using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
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
        public ClothesTypeEntity(IClothesType clothesType,
                                 string categoryName, CategoryEntity category,
                                 IEnumerable<ClothesTypeGenderCompositeEntity> clothesTypeGenderComposites, 
                                 IEnumerable<ClothesEntity> clothes)
          : this(clothesType.Name, categoryName, category, clothesTypeGenderComposites, clothes)
        { }

        public ClothesTypeEntity(string name, string categoryName,
                                 CategoryEntity? category,
                                 IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites,
                                 IEnumerable<ClothesEntity>? clothes)
           : base(name, clothes)
        {
            CategoryName = categoryName;
            Category = category;
            ClothesTypeGenderComposites = clothesTypeGenderComposites?.ToList();
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
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }

    }
}