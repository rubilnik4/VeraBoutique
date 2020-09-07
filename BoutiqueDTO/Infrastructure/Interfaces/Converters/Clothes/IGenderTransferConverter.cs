using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public interface IGenderTransferConverter : ITransferConverter<GenderType, IGenderDomain, IGenderTransfer>
    { }
}