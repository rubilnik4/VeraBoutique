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
        public ClothesTypeEntity(string name, string categoryName)
            : this(name, categoryName, null,
                   Enumerable.Empty<ClothesEntity>(),
                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>())
        { }


        public ClothesTypeEntity(IClothesType clothesType, CategoryEntity category,
                                 IEnumerable<ClothesEntity> clothes,
                                 IEnumerable<ClothesTypeGenderCompositeEntity> clothesTypeGenderComposites)
          : this(clothesType, category.Name, category, clothes, clothesTypeGenderComposites)
        { }

        public ClothesTypeEntity(IClothesType clothesType,
                                 string categoryName, CategoryEntity? category,
                                 IEnumerable<ClothesEntity>? clothes,
                                 IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites)
          : this(clothesType.Name, categoryName, category, clothes, clothesTypeGenderComposites)
        { }

        public ClothesTypeEntity(IClothesTypeShortEntity clothesTypeShort,
                                 IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites)
        : this(clothesTypeShort.Name, clothesTypeShort.CategoryName, clothesTypeShort.Category,
               clothesTypeShort.Clothes, clothesTypeGenderComposites)
        { }

        public ClothesTypeEntity(string name, string categoryName, CategoryEntity? category,
                                 IEnumerable<ClothesEntity>? clothes,
                                 IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites)
           : base(name, categoryName, category, clothes)
        {
            ClothesTypeGenderComposites = clothesTypeGenderComposites?.ToList();
        }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }

    }
}