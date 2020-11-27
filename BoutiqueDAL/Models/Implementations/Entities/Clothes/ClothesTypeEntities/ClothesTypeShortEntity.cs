using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая сущность базы данных
    /// </summary>
    public class ClothesTypeShortEntity : ClothesType, IClothesTypeShortEntity
    {
        public ClothesTypeShortEntity(IClothesType clothesType, IEnumerable<ClothesEntity> clothesEntities)
            : this(clothesType.Name, clothesEntities)
        { }

        public ClothesTypeShortEntity(string name, IEnumerable<ClothesEntity>? clothes)
           : base(name)
        {
            Clothes = clothes?.ToList();
        }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}