using System;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Models.Implementations.Connection;
using BoutiqueDTO.Models.Interfaces.Connection;
using BoutiquePrerequisites.Factories.Configuration;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiquePrerequisites.Factories.Connection
{
    /// <summary>
    /// Логин и пароль
    /// </summary>
    public static class BoutiqueAuthorize
    {
        /// <summary>
        /// Параметры сервера
        /// </summary>
        private const string BOUTIQUE_SECTION = "BoutiqueLogin";

        /// <summary>
        /// Имя сервера
        /// </summary>
        private const string USERNAME = "UserName";

        /// <summary>
        /// Имя сервера
        /// </summary>
        private const string PASSWORD = "Password";

        /// <summary>
        /// Параметры подключения к серверу Boutique
        /// </summary>
        public static IResultValue<IAuthorizeDomain> BoutiqueLogin =>
            BoutiqueLoginFunc.
            ResultCurryOkBind(BoutiqueUserName).
            ResultCurryOkBind(BoutiquePassword).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения параметров подключения
        /// </summary>
        private static IResultValue<Func<string, string, IAuthorizeDomain>> BoutiqueLoginFunc =>
            new ResultValue<Func<string, string, IAuthorizeDomain>>((userName, password) =>
                new AuthorizeDomain(userName, password));

        /// <summary>
        /// Параметры подключения к серверу Boutique
        /// </summary>
        private static IResultValue<string> BoutiqueUserName =>
            ConfigurationFactory.Configuration[$"{BOUTIQUE_SECTION}:{USERNAME}"].
            ToResultValueWhereOkBad(userName => !String.IsNullOrWhiteSpace(userName),
                                    userName => userName,
                                    _ => new ErrorResult(ErrorResultType.ValueNotFound, "Логин не найден"));

        /// <summary>
        /// Параметры подключения к серверу Boutique
        /// </summary>
        private static IResultValue<string> BoutiquePassword =>
            ConfigurationFactory.Configuration[$"{BOUTIQUE_SECTION}:{PASSWORD}"].
            ToResultValueWhereOkBad(password => !String.IsNullOrWhiteSpace(password),
                                    password => password,
                                    _ => new ErrorResult(ErrorResultType.ValueNotFound, "Пароль не найден"));


    }
}