using System;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Errors.DatabaseErrors;

namespace Functional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка дублирующего поля в базе данных
    /// </summary>
    public class DatabaseValueDuplicatedErrorResult<TValue> : DatabaseValueErrorResult<TValue>, IDatabaseValueDuplicatedErrorResult
        where TValue : notnull
    {
        public DatabaseValueDuplicatedErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        protected DatabaseValueDuplicatedErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseValueDuplicatedErrorResult<TValue>(Value, TableName, description, exception);
    }
}