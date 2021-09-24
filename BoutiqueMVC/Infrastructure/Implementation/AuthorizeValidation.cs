using System;
using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueMVC.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Infrastructure.Implementation
{
    /// <summary>
    /// Проверка логина и пароля
    /// </summary>
    public static class AuthorizeValidation
    {
        /// <summary>
        /// Проверить имя пользователя и пароль
        /// </summary>
        public static IResultValue<IAuthorizeDomain> AuthorizeValidate(IAuthorizeDomain authorize, AuthorizeSettings settings) =>
            new ResultValue<Func<string, string, IAuthorizeDomain>>(GetAuthorize).
            ResultValueCurryOk(EmailValidate(authorize.Email)).
            ResultValueCurryOk(PasswordValidate(authorize.Password, settings)).
            ResultValueOk(getAuthorize => getAuthorize.Invoke());

        /// <summary>
        /// Получить имя пользователя и пароль
        /// </summary>
        private static IAuthorizeDomain GetAuthorize(string email, string password) =>
            new AuthorizeDomain(email, password);

        /// <summary>
        /// Проверка почты
        /// </summary>
        private static IResultValue<string> EmailValidate(string email) =>
            email.ToResultValueWhere(EmailValidation.IsValid,
                                     _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Email, "Некорректный адрес почты"));

        /// <summary>
        /// Проверка пароля
        /// </summary>
        private static IResultValue<string> PasswordValidate(string password, AuthorizeSettings settings) =>
            password.ToResultValueWhere(passwordValue => PasswordValidation.IsValid(passwordValue, settings.PasswordRequiredLength,
                                                                                    settings.PasswordRequireDigit),
                                        _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password, "Некорректный пароль"));
    }
}