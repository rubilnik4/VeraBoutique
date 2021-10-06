using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Identities;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity
{
    /// <summary>
    /// Конвертер личных данных в трансферную модель
    /// </summary>
    public interface IPersonalTransferConverter : ITransferConverter<string, IPersonalDomain, PersonalTransfer>
    { }
}