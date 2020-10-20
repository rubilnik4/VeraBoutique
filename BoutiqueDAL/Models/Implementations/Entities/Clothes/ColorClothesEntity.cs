using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Цвет одежды. Сущность базы данных
    /// </summary>
    public class ColorClothesEntity : ColorClothes, IColorClothesEntity
    {
        public ColorClothesEntity(string name)
            : base(name)
        { }
    }
}