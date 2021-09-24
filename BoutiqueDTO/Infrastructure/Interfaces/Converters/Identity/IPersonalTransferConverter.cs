using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Identity;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity
{
    /// <summary>
    /// Конвертер личных данных в трансферную модель
    /// </summary>
    public interface IPersonalTransferConverter : ITransferConverter<string, IPersonalDomain, PersonalTransfer>
    { }
}