using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Interfaces.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization
{
    /// <summary>
    /// Конвертер логина и пароля в трансферную модель
    /// </summary>
    public class AuthorizeTransferConverter : TransferConverter<string, IAuthorizeDomain, AuthorizeTransfer>,
                                              IAuthorizeTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override AuthorizeTransfer ToTransfer(IAuthorizeDomain authorizeDomain) =>
            new AuthorizeTransfer(authorizeDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IAuthorizeDomain> FromTransfer(AuthorizeTransfer authorizeTransfer) =>
            new AuthorizeDomain(authorizeTransfer).
            Map(authorize => new ResultValue<IAuthorizeDomain>(authorize));
    }
}