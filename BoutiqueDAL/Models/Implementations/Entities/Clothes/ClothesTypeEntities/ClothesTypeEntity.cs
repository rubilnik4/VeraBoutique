using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
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