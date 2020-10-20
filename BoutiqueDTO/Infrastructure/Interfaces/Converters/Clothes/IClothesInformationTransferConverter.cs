using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public interface IClothesInformationTransferConverter : ITransferConverter<int, IClothesInformationDomain, ClothesInformationTransfer>
    { }
}