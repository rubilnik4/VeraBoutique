using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Identities;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity
{
    /// <summary>
    /// Конвертер логина и пароля в трансферную модель
    /// </summary>
    public interface IAuthorizeTransferConverter : ITransferConverter<string, IAuthorizeDomain, AuthorizeTransfer>
    { }
}