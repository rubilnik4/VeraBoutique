using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities
{
    /// <summary>
    /// Одежда. Базовая сущность базы данных
    /// </summary>
    public class ClothesShortEntity : ClothesShort, IClothesShortEntity
    {
        public ClothesShortEntity(IClothesShort clothesShort)
            : this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image)
        { }

        public ClothesShortEntity (int id, string name, decimal price, byte[]? image)
            : base(id, name, price, image)
        { }
    }
}