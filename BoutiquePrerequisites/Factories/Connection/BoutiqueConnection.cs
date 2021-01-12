using System;
using System.Runtime.InteropServices;
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
    /// Параметры подключения к серверу Boutique
    /// </summary>
    public static class BoutiqueConnection
    {
        /// <summary>
        /// Параметры сервера
        /// </summary>
        private const string BOUTIQUE_SECTION = "BoutiqueServer";

        /// <summary>
        /// Имя сервера
        /// </summary>
        private const string HOST = "Host";

        /// <summary>
        /// Имя сервера
        /// </summary>
        private const string TIME_OUT = "TimeOut";

        /// <summary>
        /// Параметры подключения к серверу Boutique
        /// </summary>
        public static IResultValue<IHostConnection> BoutiqueHostConnection =>
            BoutiqueConnectionFunc.
            ResultCurryOkBind(BoutiqueHost).
            ResultCurryOkBind(BoutiqueTimeOut).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения параметров подключения
        /// </summary>
        private static IResultValue<Func<Uri, int, IHostConnection>> BoutiqueConnectionFunc =>
            new ResultValue<Func<Uri, int, IHostConnection>>((host, timeOut) =>
                new HostConnection(host, TimeSpan.FromSeconds(timeOut)));
        
        /// <summary>
        /// Параметры подключения к серверу Boutique
        /// </summary>
        private static IResultValue<Uri> BoutiqueHost =>
            ConfigurationFactory.Configuration[$"{BOUTIQUE_SECTION}:{HOST}"].
            ToResultValueWhereOkBad(host => Uri.IsWellFormedUriString(host, UriKind.Absolute),
                                    host => new Uri(host),
                                    _ => new ErrorResult(ErrorResultType.ValueNotFound, "Хост не найден"));

        /// <summary>
        /// Параметры подключения к серверу Boutique
        /// </summary>
        private static IResultValue<int> BoutiqueTimeOut =>
            ConfigurationFactory.Configuration[$"{BOUTIQUE_SECTION}:{TIME_OUT}"].
            ToResultValueWhereOkBad(timeOut => Int32.TryParse(timeOut, out int _),
                                    Int32.Parse,
                                    _ => new ErrorResult(ErrorResultType.ValueNotFound, "Таймаут не найден"));
    }
}