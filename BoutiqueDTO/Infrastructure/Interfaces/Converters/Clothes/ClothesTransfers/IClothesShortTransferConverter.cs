using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Конвертер одежды в трансферную модель
    /// </summary>
    public interface IClothesShortTransferConverter : ITransferConverter<int, IClothesShortDomain, ClothesShortTransfer>
    { }
}