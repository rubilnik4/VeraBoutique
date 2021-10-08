using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Identities;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity
{
    /// <summary>
    /// Конвертер пользователей в трансферную модель
    /// </summary>
    public interface IBoutiqueUserTransferConverter : ITransferConverter<string, IBoutiqueUserDomain, BoutiqueUserTransfer>
    { }
}