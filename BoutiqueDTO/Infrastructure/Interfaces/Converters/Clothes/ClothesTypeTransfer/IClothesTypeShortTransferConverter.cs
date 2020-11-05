using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer
{
    /// <summary>
    /// Конвертер основной информации вида одежды в трансферную модель
    /// </summary>
    public interface IClothesTypeShortTransferConverter :
        ITransferConverter<string, IClothesTypeShortDomain, ClothesTypeShortTransfer>
    { }
}