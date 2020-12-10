using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public interface IClothesTypeTransfer : IClothesTypeBase<CategoryTransfer, GenderTransfer>, IClothesTypeShortTransfer
    { }
}