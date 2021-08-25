using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.AuthorizeErrors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка подключения к базе данных
    /// </summary>
    public class DatabaseConnectionErrorResult : ErrorTypeResult<DatabaseErrorType>
    {
        public DatabaseConnectionErrorResult(string parameter, string description)
          : this(parameter, description, null)
        { }

        public DatabaseConnectionErrorResult(string parameter, string description, Exception? exception)
            : base(DatabaseErrorType.Connection, description, exception)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseConnectionErrorResult(Parameter, description, exception);
    }
}