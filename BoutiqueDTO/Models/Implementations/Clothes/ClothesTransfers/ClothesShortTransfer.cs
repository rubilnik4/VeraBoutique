using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Базовая трансферная модель
    /// </summary>
    public class ClothesShortTransfer : ClothesShortBase, IClothesShortTransfer
    {
        public ClothesShortTransfer(IClothesShortBase clothes)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                   clothes.GenderType, clothes.ClothesTypeName)
        { }
        
        [JsonConstructor]
        public ClothesShortTransfer(int id, string name, string description, decimal price, byte[] image,
                                    GenderType genderType, string clothesTypeName)
            : base(id, name, description, price, image, genderType, clothesTypeName)
        { }
    }
}