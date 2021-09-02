using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

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
            ErrorResultFactory.DatabaseError(DatabaseErrorType.Save, "Тестовая ошибка базы");

        /// <summary>
        /// Тестовая ошибка подключения к базе данных
        /// </summary>
        public static IErrorResult ErrorTypeDatabaseTable =>
            ErrorResultFactory.DatabaseTableError("testTable", "Тестовая ошибка таблицы базы");
    }
}