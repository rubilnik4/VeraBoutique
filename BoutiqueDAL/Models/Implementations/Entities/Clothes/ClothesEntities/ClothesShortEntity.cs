using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities
{
    /// <summary>
    /// Одежда. Базовая сущность базы данных
    /// </summary>
    public class ClothesShortEntity : ClothesMain, IClothesShortEntity
    {
        public ClothesShortEntity(IClothesMain clothes)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image)
        { }

        public ClothesShortEntity (int id, string name,  string description, decimal price, byte[]? image)
            : base(id, name, description, price, image)
        { }
    }
}