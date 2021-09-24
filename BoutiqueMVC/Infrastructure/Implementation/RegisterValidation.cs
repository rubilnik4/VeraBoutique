using System;
using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.Net.Http.Headers;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Infrastructure.Implementation
{
    /// <summary>
    /// Проверка регистрации
    /// </summary>
    public static class RegisterValidation
    {
        /// <summary>
        /// Проверить регистрацию
        /// </summary>
        public static IResultValue<IRegisterDomain> RegisterValidate(IRegisterDomain register, AuthorizeSettings authorizeSettings) =>
            new ResultValue<Func<IAuthorizeDomain, IPersonalDomain, IRegisterDomain>>(GetRegister).
            ResultValueCurryOk(AuthorizeValidation.AuthorizeValidate(register.Authorize, authorizeSettings)).
            ResultValueCurryOk(PersonalValidation.PersonalValidate(register.Personal)).
            ResultValueOk(getAuthorize => getAuthorize.Invoke());

        /// <summary>
        /// Получить регистрацию
        /// </summary>
        private static IRegisterDomain GetRegister(IAuthorizeDomain authorize, IPersonalDomain personal) =>
            new RegisterDomain(authorize, personal);
    }
}