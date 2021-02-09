using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Models.Implementations.Environment;
using BoutiqueMVC.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
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
            ResultCurryBindOk(Login).
            ResultCurryBindOk(Email).
            ResultCurryBindOk(Phone).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        private static IResultValue<IAuthorizeDomain> Login =>
            new ResultValue<Func<string, string, IAuthorizeDomain>>(GetAuthorizeLogin).
            ResultCurryBindOk(UserName).
            ResultCurryBindOk(Password).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Получить пользователя о умолчанию
        /// </summary>
        private static BoutiqueUser GetDefaultUser(IAuthorizeDomain authorizeDomain, string email, string phone) =>
            new BoutiqueUser(IdentityRoleTypes.ADMIN_ROLE, authorizeDomain, email, phone);

        /// <summary>
        /// Получить имя пользователя и пароль
        /// </summary>
        private static IAuthorizeDomain GetAuthorizeLogin(string userName, string password) =>
            new AuthorizeDomain(userName, password);

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private static IResultValue<string> UserName =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.USER_NAME).
            ToResultValueNullCheck(new ErrorResult(ErrorResultType.UserDefaultNotFound, "Имя пользователя не найдено"));

        /// <summary>
        /// Пароль
        /// </summary>
        private static IResultValue<string> Password =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PASSWORD).
            ToResultValueNullCheck(new ErrorResult(ErrorResultType.UserDefaultNotFound, "Пароль пользователя не найден"));

        /// <summary>
        /// Почта
        /// </summary>
        private static IResultValue<string> Email =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.EMAIL).
            ToResultValueNullCheck(new ErrorResult(ErrorResultType.UserDefaultNotFound, "Почта пользователя не найдена"));

        /// <summary>
        /// Телефон
        /// </summary>
        private static IResultValue<string> Phone =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PHONE).
            ToResultValueNullCheck(new ErrorResult(ErrorResultType.UserDefaultNotFound, "Телефон пользователя не найден"));
    }
}