using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities
{
    public class ClothesTypeShortEntity : ClothesTypeEntity, IClothesTypeShortEntity
    {
        public ClothesTypeShortEntity(string name, string categoryName)
          : this(name, categoryName, null, null)
        { }

        public ClothesTypeShortEntity(string name, string categoryName, CategoryEntity? categoryEntity,
                                      ClothesTypeGenderCompositeEntity? clothesTypeGenderEntity)
           : base(name, categoryName, categoryEntity)
        {
            ClothesTypeGender = clothesTypeGenderEntity;
        }

        /// <summary>
        /// Связующая сущность пола и вида одежды
        /// </summary>
        public ClothesTypeGenderCompositeEntity? ClothesTypeGender { get; }
    }
}