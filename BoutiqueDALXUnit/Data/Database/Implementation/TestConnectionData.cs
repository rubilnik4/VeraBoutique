using BoutiqueDAL.Models.Implementations.Connection;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Данные для проверки подключения
    /// </summary>
    public static class TestConnectionData
    {
        /// <summary>
        /// Параметры подключения к базе данных
        /// </summary>
        public static DatabaseConnection DatabaseConnection =>
            new(HostConnection, Database, Authorization);

        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static HostConnection HostConnection =>
            new("localhost", 5432);

        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static Authorization Authorization =>
            new("username", "password");

        /// <summary>
        /// Корректные параметры подключения
        /// </summary>
        public static string Database =>
            "database";
    }
}