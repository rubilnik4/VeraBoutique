using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfer
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public interface IClothesTransferConverter : ITransferConverter<int, IClothesDomain, Models.Implementations.Clothes.ClothesTransfers.ClothesTransfer>
    { }
}