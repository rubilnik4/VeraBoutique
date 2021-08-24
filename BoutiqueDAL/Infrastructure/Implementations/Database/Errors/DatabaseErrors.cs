using System;
using System.Collections.Generic;
using Functional.Models.Enums;
using Functional.Models.Implementations.Results;
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
            new ErrorTypeResult<>(ErrorResultType.DatabaseSave, "Ошибка сохранения базы");

        /// <summary>
        /// Ошибка доступа
        /// </summary>
        public static IErrorResult TableAccessError(string tableName) =>
            new ErrorTypeResult<>(ErrorResultType.DatabaseTableAccess, $"Ошибка доступа к таблице {tableName}");

        /// <summary>
        /// Элемент не найден
        /// </summary>
        public static IErrorResult ValueNotFoundError<TId>(TId id, string tableName) 
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.ValueNotFound, $"Элемент {id} в таблице {tableName} не найден");

        /// <summary>
        /// Элементы не найден
        /// </summary>
        public static IErrorResult ValuesNotFoundError<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.ValueNotFound, $"Элементы {AggregateIdsToString(ids)} в таблице {tableName} не найден");

        /// <summary>
        /// Дублирование элемента
        /// </summary>
        public static IErrorResult DuplicateError<TId>(TId id, string tableName)
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.DatabaseValueDuplicate, $"Дублирование элемента {id} в таблице {tableName}");

        /// <summary>
        /// Дублирование элементов
        /// </summary>
        public static IErrorResult DuplicateErrors<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.DatabaseValueDuplicate, $"Дублирование элементов {AggregateIdsToString(ids)} в таблице {tableName}");

        /// <summary>
        /// Коллекция пуста
        /// </summary>
        public static IErrorResult CollectionEmpty(string collectionType, string tableName) =>
            new ErrorTypeResult<>(ErrorResultType.CollectionEmpty, $"Коллекция типа {collectionType} в таблице {tableName} пуста");

        /// <summary>
        /// Преобразовать список элементов в строку
        /// </summary>
        private static string AggregateIdsToString<TId>(IEnumerable<TId> ids)
            where TId : notnull =>
            String.Join(",", ids);
    }
}