using System;
using System.Net.Http;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.AuthorizeErrors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Implementations.Errors.ConvertionErrors;
using Functional.Models.Implementations.Errors.DatabaseErrors;
using Functional.Models.Implementations.Errors.RestErrors;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace Functional.Models.Implementations.Errors
{
    /// <summary>
    /// Фабрика создания типов ошибок
    /// </summary>
    public static class ErrorResultFactory
    {
        /// <summary>
        /// Создать ошибку общего типа
        /// </summary>
        public static IErrorResult CommonError(CommonErrorType commonErrorType, string description) =>
            new CommonErrorResult(commonErrorType, description);

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
            new DatabaseValueNotFoundErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Создать ошибку некорректного значения в базе данных
        /// </summary>
        public static IErrorResult DatabaseValueNotValidError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueNotValidErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Создать ошибку дублирующего значения в базе данных
        /// </summary>
        public static IErrorResult DatabaseValueDuplicateError<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            new DatabaseValueDuplicatedErrorResult<TValue>(value, tableName, description);

        /// <summary>
        /// Создать ошибку сериализации
        /// </summary>
        public static IErrorResult SerializeError<TValue>(ConvertionErrorType convertionErrorType, TValue value, string description)
            where TValue : notnull =>
            new SerializeErrorResult<TValue>(convertionErrorType, value, description);

        /// <summary>
        /// Создать ошибку сериализации
        /// </summary>
        public static IErrorResult DeserializeError<TValue>(ConvertionErrorType convertionErrorType, string value, string description)
            where TValue : notnull =>
            new DeserializeErrorResult<TValue>(convertionErrorType, value, description);

        /// <summary>
        /// Создать ошибку с типом данных
        /// </summary>
        public static IErrorResult ErrorType<TError>(TError error, string description)
            where TError : struct =>
            new ErrorTypeResult<TError>(error, description);

        /// <summary>
        /// Создать ошибку отсутствующего значения
        /// </summary>
        public static IErrorResult ValueNotFoundError<TValue, TType>(TValue? value, TType parentType)
            where TValue : class
            where TType : Type =>
            new ValueNotFoundErrorResult<TValue, TType>(nameof(TValue));

        /// <summary>
        /// Создать ошибку неверного значения
        /// </summary>
        public static IErrorResult ValueNotValidError<TValue, TType>(TValue value, TType parentType, string description)
            where TValue : notnull
            where TType : Type =>
            new ValueNotValidErrorResult<TValue, TType>(value, nameof(TValue));

        /// <summary>
        /// Создать ошибку REST сервера
        /// </summary>
        public static IErrorResult RestError(RestErrorType restErrorType, string host, string description) =>
            new RestErrorResult(restErrorType, host, description);

        /// <summary>
        /// Создать ошибку REST сервера с сообщением
        /// </summary>
        public static IErrorResult RestError(RestErrorType restErrorType, HttpResponseMessage httpResponseMessage, string description) =>
            new RestMessageErrorResult(restErrorType, httpResponseMessage, description);
    }
}