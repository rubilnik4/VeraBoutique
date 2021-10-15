using System;
using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Infrastructure.Implementation.Validation.Identities
{
    /// <summary>
    /// Проверка регистрации
    /// </summary>
    public static class RegisterValidation
    {
        /// <summary>
        /// Проверить регистрацию
        /// </summary>
        public static IResultValue<IRegisterDomain> RegisterValidate(IRegisterDomain register, PasswordValidateSettings passwordSettings) =>
            new ResultValue<Func<IAuthorizeDomain, IPersonalDomain, IRegisterDomain>>(GetRegister).
            ResultValueCurryOk(AuthorizeValidation.AuthorizeValidate(register.Authorize, passwordSettings)).
            ResultValueCurryOk(PersonalValidation.PersonalValidate(register.Personal)).
            ResultValueOk(getAuthorize => getAuthorize.Invoke());

        /// <summary>
        /// Получить регистрацию
        /// </summary>
        private static IRegisterDomain GetRegister(IAuthorizeDomain authorize, IPersonalDomain personal) =>
            new RegisterDomain(authorize, personal);
    }
}