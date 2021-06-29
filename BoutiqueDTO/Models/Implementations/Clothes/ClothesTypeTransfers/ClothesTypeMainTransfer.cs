using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Основная модель. Трансферная модель
    /// </summary>
    public class ClothesTypeMainTransfer: ClothesTypeMainBase<CategoryTransfer>, IClothesTypeMainTransfer
    {
        public ClothesTypeMainTransfer(IClothesTypeBase clothesType, CategoryTransfer category)
         : base(clothesType.Name, clothesType.SizeTypeDefault, category)
        { }

        [JsonConstructor]
        public ClothesTypeMainTransfer(string name, SizeType sizeTypeDefault, CategoryTransfer category)
          : base(name, sizeTypeDefault, category)
        { }
    }
}