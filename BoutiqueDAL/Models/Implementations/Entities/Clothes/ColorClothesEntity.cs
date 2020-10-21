using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Цвет одежды. Сущность базы данных
    /// </summary>
    public class ColorClothesEntity : ColorClothes, IColorClothesEntity
    {
        public ColorClothesEntity(string name)
            : this(name, Enumerable.Empty<ClothesColorCompositeEntity>())
        { }
        
        public ColorClothesEntity(string name, IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities)
            : base(name)
        {
            ClothesColorCompositeEntities = clothesColorCompositeEntities.ToList();
        }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity> ClothesColorCompositeEntities { get; }
    }
}