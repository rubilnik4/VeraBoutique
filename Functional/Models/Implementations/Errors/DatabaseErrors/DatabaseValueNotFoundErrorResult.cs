using System;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка отсутствующего поля в базе данных
    /// </summary>
    public class DatabaseValueErrorResult<TValue> : DatabaseTableErrorResult
        where TValue : notnull
    {
        public DatabaseValueErrorResult(DatabaseErrorType databaseErrorType, TValue value, string tableName, string description)
            : this(databaseErrorType, value, tableName, description, null)
        { }

        protected DatabaseValueErrorResult(DatabaseErrorType databaseErrorType, TValue value, string tableName, 
                                           string description, Exception? exception)
           : base(databaseErrorType, tableName, description, exception)
        {
            Value = value;
        }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseValueErrorResult<TValue>(ErrorType, Value, TableName, description, exception);
    }
}