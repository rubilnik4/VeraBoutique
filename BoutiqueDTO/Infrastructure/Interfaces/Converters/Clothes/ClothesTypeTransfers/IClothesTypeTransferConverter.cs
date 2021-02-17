using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер полной информации вида одежды в трансферную модель
    /// </summary>
    public interface IClothesTypeTransferConverter : ITransferConverter<string, IClothesTypeDomain, ClothesTypeTransfer>
    { }
}