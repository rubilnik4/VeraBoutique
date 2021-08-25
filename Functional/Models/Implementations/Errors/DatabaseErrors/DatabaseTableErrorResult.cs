using System;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Interfaces.Errors;

namespace Functional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка таблицы базы данных
    /// </summary>
    public class DatabaseTableErrorResult : ErrorTypeResult<DatabaseErrorType>
    {
        public DatabaseTableErrorResult(string tableName, string description)
          : this(DatabaseErrorType.TableAccess, tableName, description)
        { }

        protected DatabaseTableErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        protected DatabaseTableErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description,
                                           Exception? exception)
            : base(databaseErrorType, description, exception)
        {
            TableName = tableName;
        }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseTableErrorResult(DatabaseErrorType.TableAccess, TableName, description, exception);
    }
}