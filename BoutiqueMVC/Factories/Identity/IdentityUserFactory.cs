using System;
using BoutiqueMVC.Models.Implementations.Authentication;
using BoutiqueMVC.Models.Implementations.Environment;
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
    public static class IdentityUserFactory
    {
        /// <summary>
        /// Пользователь по умолчанию
        /// </summary>
        public static IResultValue<IdentityUser> DefaultUser =>
            new ResultValue<Func<Login, string, string, IdentityUser>>(GetDefaultUser).
            ResultCurryOkBind(Login).
            ResultCurryOkBind(Email).
            ResultCurryOkBind(Phone).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        private static IResultValue<Login> Login =>
            new ResultValue<Func<string, string, Login>>(GetLogin).
                ResultCurryOkBind(UserName).
                ResultCurryOkBind(Password).
                ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Получить пользователя о умолчанию
        /// </summary>
        private static IdentityUser GetDefaultUser(Login login, string email, string phone) =>
            new IdentityUser
            {
                UserName = login.UserName,
                NormalizedUserName = login.UserName.ToUpperInvariant(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                PhoneNumber = phone,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            }.Map(user =>
            {
                user.PasswordHash = GetPasswordHash(user, login.Password);
                return user;
            });

        /// <summary>
        /// Получить имя пользователя и пароль
        /// </summary>
        private static Login GetLogin(string userName, string password) =>
            new Login(userName, password);

        /// <summary>
        /// Хэшировать пароль
        /// </summary>
        private static string GetPasswordHash(IdentityUser user, string password) =>
            new PasswordHasher<IdentityUser>().
            Map(passwordHasher => passwordHasher.HashPassword(user, password));

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