using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер полной информации вида одежды в трансферную модель
    /// </summary>
    public interface IClothesTypeMainTransferConverter : ITransferConverter<string, IClothesTypeMainDomain, ClothesTypeMainTransfer>
    { }
}