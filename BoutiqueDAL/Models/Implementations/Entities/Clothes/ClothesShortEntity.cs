using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Сущность базы данных
    /// </summary>
    public class ClothesShortEntity : ClothesShort, IClothesShortEntity
    {
        public ClothesShortEntity(int id, string name, decimal price, byte[]? image)
            : base(id, name, price, image)
        { }
    }
}