using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер одежды в трансферную модель
    /// </summary>
    public interface IClothesShortTransferConverter : ITransferConverter<int, IClothesShortDomain, ClothesShortTransfer>
    { }
}