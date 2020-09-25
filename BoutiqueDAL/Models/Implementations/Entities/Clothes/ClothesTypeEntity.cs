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
            : this(name, Enumerable.Empty<ClothesTypeGenderEntity>())
        { }

        public ClothesTypeEntity(string name, IEnumerable<ClothesTypeGenderEntity> clothesTypeGenderEntities)
           : base(name)
        {
            ClothesTypeGenderEntities = clothesTypeGenderEntities.ToList();
        }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderEntity> ClothesTypeGenderEntities { get; }
    }
}