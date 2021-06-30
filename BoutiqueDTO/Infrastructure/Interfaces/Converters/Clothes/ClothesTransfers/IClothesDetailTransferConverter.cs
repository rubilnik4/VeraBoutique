using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers
{
    /// <summary>
    /// Конвертер уточненной информации об одежде в трансферную модель
    /// </summary>
    public interface IClothesDetailTransferConverter : ITransferConverter<int, IClothesDetailDomain, ClothesDetailTransfer>
    { }
}