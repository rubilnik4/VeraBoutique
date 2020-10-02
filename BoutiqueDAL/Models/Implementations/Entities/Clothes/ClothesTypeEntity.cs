using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public class ClothesTypeEntity : ClothesType, IClothesTypeEntity
    {
        public ClothesTypeEntity(string name)
            : this(name, null, Enumerable.Empty<ClothesTypeGenderEntity>())
        { }

        public ClothesTypeEntity(string name, CategoryEntity? categoryEntity, 
                                 IEnumerable<ClothesTypeGenderEntity> clothesTypeGenderEntities)
           : base(name)
        {
            CategoryEntity = categoryEntity;
            ClothesTypeGenderEntities = clothesTypeGenderEntities.ToList();
        }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public CategoryEntity? CategoryEntity { get; }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderEntity> ClothesTypeGenderEntities { get; }
    }
}