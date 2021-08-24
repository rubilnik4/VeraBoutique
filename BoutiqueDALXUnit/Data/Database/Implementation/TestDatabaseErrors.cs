using Functional.Models.Enums;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Ошибки баз данных
    /// </summary>
    public class TestDatabaseErrors
    {
        /// <summary>
        /// Тестовая ошибка подключения к базе данных
        /// </summary>
        public static IErrorResult ErrorTypeDatabase =>
            new ErrorResult(ErrorResultType.DatabaseIncorrectConnection, "Тестовая ошибка базы");

        /// <summary>
        /// Тестовая ошибка подключения к базе данных
        /// </summary>
        public static IErrorResult ErrorTypeDatabaseTable =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, "Тестовая ошибка таблицы базы");
    }
}