using System;
using System.Collections.Generic;
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
            new ErrorResult(ErrorResultType.DatabaseSave, "Ошибка сохранения базы");

        /// <summary>
        /// Ошибка доступа
        /// </summary>
        public static IErrorResult TableAccessError(string tableName) =>
            new ErrorResult(ErrorResultType.DatabaseTableAccess, $"Ошибка доступа к таблице {tableName}");

        /// <summary>
        /// Элемент не найден
        /// </summary>
        public static IErrorResult ValueNotFoundError<TId>(TId id, string tableName) 
            where TId : notnull =>
            new ErrorResult(ErrorResultType.ValueNotFound, $"Элемент {id} в таблице {tableName} не найден");

        /// <summary>
        /// Элементы не найден
        /// </summary>
        public static IErrorResult ValuesNotFoundError<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            new ErrorResult(ErrorResultType.ValueNotFound, $"Элементы {AggregateIdsToString(ids)} в таблице {tableName} не найден");

        /// <summary>
        /// Дублирование элемента
        /// </summary>
        public static IErrorResult DuplicateError<TId>(TId id, string tableName)
            where TId : notnull =>
            new ErrorResult(ErrorResultType.DatabaseValueDuplicate, $"Дублирование элемента {id} в таблице {tableName}");

        /// <summary>
        /// Дублирование элементов
        /// </summary>
        public static IErrorResult DuplicateErrors<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            new ErrorResult(ErrorResultType.DatabaseValueDuplicate, $"Дублирование элементов {AggregateIdsToString(ids)} в таблице {tableName}");

        /// <summary>
        /// Преобразовать список элементов в строку
        /// </summary>
        private static string AggregateIdsToString<TId>(IEnumerable<TId> ids)
            where TId : notnull =>
            String.Join(",", ids);
    }
}