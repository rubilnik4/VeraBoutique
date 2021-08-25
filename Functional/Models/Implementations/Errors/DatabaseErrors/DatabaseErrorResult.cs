using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка базы данных
    /// </summary>
    public class DatabaseErrorResult : ErrorTypeResult<DatabaseErrorType>
    {
        public DatabaseErrorResult(DatabaseErrorType databaseErrorType, string description)
            : this(databaseErrorType, description, null)
        { }

        public DatabaseErrorResult(DatabaseErrorType databaseErrorType, string description, Exception? exception)
            : base(databaseErrorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseErrorResult(ErrorType, description, exception);
    }
}