using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfer;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfer
{
    /// <summary>
    /// Конвертер одежды в трансферную модель
    /// </summary>
    public interface IClothesShortTransferConverter : ITransferConverter<int, IClothesShortDomain, ClothesShortTransfer>
    { }
}