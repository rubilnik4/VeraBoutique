using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Factories.Implementations.Database.Errors
{
    /// <summary>
    /// Ошибки базы данных
    /// </summary>
    public static class DatabaseErrors
    {
        /// <summary>
        /// ОШибка доступа
        /// </summary>
        public static IErrorResult TableAccessError(string tableName) =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, $"Ошибка доступа к таблице {tableName}");
    }
}