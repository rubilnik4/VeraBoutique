using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    public class ClothesTypeTransfer : ClothesTypeBase, IClothesTypeTransfer
    {
        public ClothesTypeTransfer(IClothesTypeBase clothesType)
            : this(clothesType.Name, clothesType.SizeTypeDefault, clothesType.CategoryName)
        { }

        [JsonConstructor]
        public ClothesTypeTransfer(string name, SizeType sizeTypeDefault, string categoryName) 
            : base(name, sizeTypeDefault, categoryName)
        { }
    }
}