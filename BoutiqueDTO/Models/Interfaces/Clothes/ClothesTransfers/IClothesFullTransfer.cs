using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Трансферная модель
    /// </summary>
    public interface IClothesFullTransfer :
        IClothesFullBase<GenderTransfer, ClothesTypeTransfer, ColorTransfer, SizeGroupFullTransfer, SizeTransfer>,
        IClothesTransfer
    { }
}