using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.AuthorizeErrors;
using Functional.Models.Implementations.Errors.DatabaseErrors;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors
{
    /// <summary>
    /// Фабрика создания типов ошибок
    /// </summary>
    public static class ErrorResultFactory
    {
        /// <summary>
        /// Создать ошибку авторизации
        /// </summary>
        public static IErrorResult AuthorizeError(AuthorizeErrorType authorizeErrorType, string description) =>
            new AuthorizeErrorResult(authorizeErrorType, description);

        /// <summary>
        /// Создать ошибку подключения к базе данных
        /// </summary>
        public static IErrorResult DatabaseConnectionError(string parameter, string description) =>
            new DatabaseConnectionErrorResult(parameter, description);

        /// <summary>
        /// Создать ошибку базы данных
        /// </summary>
        public static IErrorResult DatabaseError(DatabaseErrorType databaseErrorType, string description) =>
            new DatabaseErrorResult(databaseErrorType, description);

        /// <summary>
        /// Создать ошибку таблицы базы данных
        /// </summary>
        public static IErrorResult DatabaseTableError(string tableName, string description) =>
            new DatabaseTableErrorResult(tableName, description);

        /// <summary>
        /// Создать ошибку отсутствующего значения в базе данных
        /// </summary>
        public static IErrorResult DatabaseValueNotFoundError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueErrorResult<TValue>(DatabaseErrorType.ValueNotFound, value, tableName, description);

        /// <summary>
        /// Создать ошибку некорректного значения в базе данных
        /// </summary>
        public static IErrorResult DatabaseValueNotValidError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueErrorResult<TValue>(DatabaseErrorType.ValueNotValid, value, tableName, description);

        /// <summary>
        /// Создать ошибку дублирующего значения в базе данных
        /// </summary>
        public static IErrorResult DatabaseValueDuplicateError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueErrorResult<TValue>(DatabaseErrorType.ValueDuplicate, value, tableName, description);
    }
}