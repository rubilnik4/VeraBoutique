using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Уточненная информация. Трансферная модель
    /// </summary>
    public interface IClothesDetailTransfer :
        IClothesDetailBase<ColorTransfer, SizeGroupMainTransfer, SizeTransfer>,
        IClothesTransfer
    { }
}