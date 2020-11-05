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
    public class ClothesTypeFullEntity : ClothesTypeEntity, IClothesTypeFullEntity
    {
        public ClothesTypeFullEntity(string name, string categoryName)
            : this(name, categoryName, null,
                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>(), 
                   Enumerable.Empty<ClothesEntity>())
        { }

        public ClothesTypeFullEntity(string name, string categoryName, CategoryEntity? category,
                                     IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites,
                                     IEnumerable<ClothesEntity>? clothes)
           : base(name, categoryName, category)
        {
            ClothesTypeGenderComposites = clothesTypeGenderComposites?.ToList();
            Clothes = clothes?.ToList();
        }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}