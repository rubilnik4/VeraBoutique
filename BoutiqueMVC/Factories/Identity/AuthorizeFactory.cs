using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Environment;
using BoutiqueMVC.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Factories.Identity
{
    /// <summary>
    /// Получить пользователя администратора
    /// </summary>
    public static class AuthorizeFactory
    {
        /// <summary>
        /// Пользователи по умолчанию
        /// </summary>
        public static IResultCollection<BoutiqueUser> DefaultUsers =>
            DefaultAdminUser.ResultValueOkToCollection(adminUser => new List<BoutiqueUser> { adminUser });

        /// <summary>
        /// Пользователь администратор по умолчанию
        /// </summary>
        private static IResultValue<BoutiqueUser> DefaultAdminUser =>
            new ResultValue<Func<IAuthorizeDomain, string, string, BoutiqueUser>>(GetDefaultUser).
            ResultValueCurryOk(Login).
            ResultValueCurryOk(Email).
            ResultValueCurryOk(Phone).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        private static IResultValue<IAuthorizeDomain> Login =>
            new ResultValue<Func<string, string, IAuthorizeDomain>>(GetAuthorizeLogin).
            ResultValueCurryOk(Username).
            ResultValueCurryOk(Password).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Получить пользователя о умолчанию
        /// </summary>
        private static BoutiqueUser GetDefaultUser(IAuthorizeDomain authorizeDomain, string email, string phone) =>
            new(IdentityRoleTypes.ADMIN_ROLE, authorizeDomain, email, phone);

        /// <summary>
        /// Получить имя пользователя и пароль
        /// </summary>
        private static IAuthorizeDomain GetAuthorizeLogin(string userName, string password) =>
            new AuthorizeDomain(userName, password);

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private static IResultValue<string> Username =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.USER_NAME).
            ToResultValueNullCheck(ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Username, "Имя пользователя по умолчанию не найдено"));

        /// <summary>
        /// Пароль
        /// </summary>
        private static IResultValue<string> Password =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PASSWORD).
            ToResultValueNullCheck(ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password, "Пароль умолчанию не найден"));

        /// <summary>
        /// Почта
        /// </summary>
        private static IResultValue<string> Email =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.EMAIL).
            ToResultValueNullCheck(ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Email, "Почта умолчанию не найдена"));

        /// <summary>
        /// Телефон
        /// </summary>
        private static IResultValue<string> Phone =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PHONE).
            ToResultValueNullCheck(ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Phone, "Телефон умолчанию не найден"));
    }
}