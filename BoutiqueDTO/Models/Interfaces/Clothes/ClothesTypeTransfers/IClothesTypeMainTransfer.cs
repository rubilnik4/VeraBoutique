using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Основная модель. Трансферная модель
    /// </summary>
    public interface IClothesTypeMainTransfer : IClothesTypeMainBase<CategoryTransfer>, IClothesTypeTransfer
    { }
}