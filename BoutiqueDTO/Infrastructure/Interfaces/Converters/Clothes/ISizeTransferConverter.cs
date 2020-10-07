using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер размеров одежды в трансферную модель
    /// </summary>
    public interface ISizeTransferConverter : ITransferConverter<(SizeType, string), ISizeDomain, SizeTransfer>
    { }
}