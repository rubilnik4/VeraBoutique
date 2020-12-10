using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Цвет одежды. Сущность базы данных
    /// </summary>
    public class ColorEntity : ColorBase, IColorEntity
    {
        public ColorEntity(IColorBase color)
           : this(color.Name)
        { }
        
        public ColorEntity(string name)
            : this(name, null)
        { }
        
        public ColorEntity(string name, IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites)
            : base(name)
        {
            ClothesColorComposites = clothesColorComposites?.ToList();
        }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity>? ClothesColorComposites { get; }
    }
}