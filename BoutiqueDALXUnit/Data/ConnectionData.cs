using BoutiqueDAL.Models.Implementations.Connection;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Данные для проверки подключения
    /// </summary>
    public static class ConnectionData
    {
        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static HostConnection HostConnectionOk => new HostConnection("localhost", 5432);

        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static Authorization AuthorizationOk => new Authorization("username", "password");

        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static string DatabaseOk => "database";

        /// <summary>
        /// Ошибка подключения к базе
        /// </summary>
        public static IErrorResult ErrorConnection =>
            new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "ErrorConnection");
    }
}