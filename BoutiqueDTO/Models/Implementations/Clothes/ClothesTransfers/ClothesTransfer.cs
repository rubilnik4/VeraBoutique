using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Базовая трансферная модель
    /// </summary>
    public class ClothesTransfer : ClothesBase, IClothesTransfer
    {
        public ClothesTransfer(IClothesBase clothes)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                   clothes.GenderType, clothes.ClothesTypeName)
        { }
        
        [JsonConstructor]
        public ClothesTransfer(int id, string name, string description, decimal price, byte[] image,
                                    GenderType genderType, string clothesTypeName)
            : base(id, name, description, price, image, genderType, clothesTypeName)
        { }
    }
}