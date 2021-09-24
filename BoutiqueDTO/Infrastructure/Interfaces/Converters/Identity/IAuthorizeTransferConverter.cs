using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Identity;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity
{
    /// <summary>
    /// Конвертер логина и пароля в трансферную модель
    /// </summary>
    public interface IAuthorizeTransferConverter : ITransferConverter<string, IAuthorizeDomain, AuthorizeTransfer>
    { }
}