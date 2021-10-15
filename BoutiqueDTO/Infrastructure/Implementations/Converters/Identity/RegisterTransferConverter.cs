using System;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Identity
{
    /// <summary>
    /// Конвертер регистрации в трансферную модель
    /// </summary>
    public class RegisterTransferConverter : TransferConverter<string, IRegisterDomain, RegisterTransfer>, IRegisterTransferConverter
    {
        public RegisterTransferConverter(IAuthorizeTransferConverter authorizeTransferConverter,  
                                         IPersonalTransferConverter personalTransferConverter)
        {
            _authorizeTransferConverter = authorizeTransferConverter;
            _personalTransferConverter = personalTransferConverter;
        }

        /// <summary>
        /// Конвертер логина и пароля в трансферную модель
        /// </summary>
        private readonly IAuthorizeTransferConverter _authorizeTransferConverter;

        /// <summary>
        /// Конвертер личных данных в трансферную модель
        /// </summary>
        private readonly IPersonalTransferConverter _personalTransferConverter;


        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override RegisterTransfer ToTransfer(IRegisterDomain registerDomain) =>
            new RegisterTransfer(_authorizeTransferConverter.ToTransfer(registerDomain.Authorize),
                                 _personalTransferConverter.ToTransfer(registerDomain.Personal));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IRegisterDomain> FromTransfer(RegisterTransfer registerTransfer) =>
            GetRegisterFunc().
            ResultValueCurryOk(_authorizeTransferConverter.GetDomain(registerTransfer.Authorize)).
            ResultValueCurryOk(_personalTransferConverter.GetDomain(registerTransfer.Personal)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения одежды
        /// </summary>
        private static IResultValue<Func<IAuthorizeDomain, IPersonalDomain, IRegisterDomain>> GetRegisterFunc() =>
            new ResultValue<Func<IAuthorizeDomain, IPersonalDomain, IRegisterDomain>>(
                (authorize, personal) => new RegisterDomain(authorize, personal));
    }
}