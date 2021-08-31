﻿using System;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Errors.DatabaseErrors;

namespace Functional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка отсутствующего поля в базе данных
    /// </summary>
    public class DatabaseValueNotFoundErrorResult<TValue> : DatabaseValueErrorResult<TValue>, IDatabaseValueNotFoundErrorResult
        where TValue : notnull
    {
        public DatabaseValueNotFoundErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        protected DatabaseValueNotFoundErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseValueNotFoundErrorResult<TValue>(Value, TableName, description, exception);
    }
}