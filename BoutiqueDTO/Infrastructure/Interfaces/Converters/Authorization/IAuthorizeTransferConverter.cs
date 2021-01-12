using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization
{
    /// <summary>
    /// Конвертер логина и пароля в трансферную модель
    /// </summary>
    public interface IAuthorizeTransferConverter : ITransferConverter<(string, string), IAuthorizeDomain, AuthorizeTransfer>
    { }
}