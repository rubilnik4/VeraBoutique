using BoutiqueDAL.Models.Implementations.Connection;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Данные для проверки подключения
    /// </summary>
    public static class TestConnectionData
    {
        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static HostConnection HostConnectionOk => new ("localhost", 5432);

        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static Authorization AuthorizationOk => new ("username", "password");

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