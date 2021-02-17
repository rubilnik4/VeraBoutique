using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes
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