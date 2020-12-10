using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая сущность базы данных
    /// </summary>
    public class ClothesTypeShortEntity : ClothesTypeShortBase, IClothesTypeShortEntity
    {
        public ClothesTypeShortEntity(IClothesTypeShortBase clothesTypeShort)
            : this(clothesTypeShort.Name, clothesTypeShort.CategoryName, null)
        { }

        public ClothesTypeShortEntity(string name, string categoryName, IEnumerable<ClothesEntity>? clothes)
           : base(name, categoryName)
        {
            Clothes = clothes?.ToList();
        }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}