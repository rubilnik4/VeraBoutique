using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Базовая трансферная модель
    /// </summary>
    public class ClothesTypeShortTransfer : ClothesTypeShortBase, IClothesTypeShortTransfer
    {
        public ClothesTypeShortTransfer(IClothesTypeShortBase clothesType)
          : this(clothesType.Name, clothesType.CategoryName)
        { }

        [JsonConstructor]
        public ClothesTypeShortTransfer(string name, string categoryName)
            :base(name, categoryName)
        { }
    }
}