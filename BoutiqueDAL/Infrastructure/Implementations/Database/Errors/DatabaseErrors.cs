using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Errors
{
    /// <summary>
    /// Ошибки базы данных
    /// </summary>
    public static class DatabaseErrors
    {
        /// <summary>
        /// Ошибка сохранения изменений
        /// </summary>
        public static IErrorResult DatabaseSaveError() =>
            new ErrorResult(ErrorResultType.DatabaseSave, $"Ошибка сохранения базы");

        /// <summary>
        /// Ошибка доступа
        /// </summary>
        public static IErrorResult TableAccessError(string tableName) =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, $"Ошибка доступа к таблице {tableName}");

        /// <summary>
        /// Элемент не найден
        /// </summary>
        public static IErrorResult ValueNotFoundError(string id, string tableName) =>
            new ErrorResult(ErrorResultType.DatabaseValueNotFound, $"Элемент {id} в таблице {tableName} не найден");
    }
}