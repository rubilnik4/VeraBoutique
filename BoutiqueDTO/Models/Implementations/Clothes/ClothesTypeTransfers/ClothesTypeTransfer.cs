using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    public class ClothesTypeTransfer : ClothesTypeBase, IClothesTypeTransfer
    {
        public ClothesTypeTransfer(IClothesTypeBase clothesType)
            : this(clothesType.Name, clothesType.CategoryName)
        { }

        [JsonConstructor]
        public ClothesTypeTransfer(string name, string categoryName) 
            : base(name, categoryName)
        { }
    }
}