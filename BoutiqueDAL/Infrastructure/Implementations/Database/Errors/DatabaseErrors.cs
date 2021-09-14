using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.DatabaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

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
        public static IErrorResult SaveError() =>
            ErrorResultFactory.DatabaseSaveError("Ошибка сохранения базы");

        /// <summary>
        /// Ошибка доступа
        /// </summary>
        public static IErrorResult AccessError(string tableName) =>
            ErrorResultFactory.DatabaseAccessError(tableName, $"Ошибка доступа к таблице {tableName}");

        /// <summary>
        /// Элемент не найден
        /// </summary>
        public static IErrorResult ValueNotFoundError<TId>(TId id, string tableName) 
            where TId : notnull =>
            ErrorResultFactory.DatabaseValueNotFoundError(id, tableName, $"Элемент {id} в таблице {tableName} не найден");

        /// <summary>
        /// Элементы не найден
        /// </summary>
        public static IErrorResult ValuesNotFoundError<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            ids.ToList().
            Map(collection => ErrorResultFactory.DatabaseValueNotFoundError(collection, tableName, 
                                    $"Элементы {AggregateIdsToString(collection)} в таблице {tableName} не найден"));

        /// <summary>
        /// Дублирование элемента
        /// </summary>
        public static IErrorResult DuplicateError<TId>(TId id, string tableName)
            where TId : notnull =>
            ErrorResultFactory.DatabaseValueDuplicateError(id, tableName, $"Дублирование элемента {id} в таблице {tableName}");

        /// <summary>
        /// Дублирование элементов
        /// </summary>
        public static IErrorResult DuplicateErrors<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            ids.ToList().
            Map(collection => ErrorResultFactory.DatabaseValueDuplicateError(collection, tableName,
                                    $"Дублирование элементов {AggregateIdsToString(collection)} в таблице {tableName}"));

        /// <summary>
        /// Коллекция пуста
        /// </summary>
        public static IErrorResult CollectionEmpty<TId>(IEnumerable<TId> ids, string tableName)
            where TId : notnull =>
            ErrorResultFactory.DatabaseValueNotValidError(ids, tableName, $"Коллекция типа {ids.GetType().Name} в таблице {tableName} пуста");

        /// <summary>
        /// Преобразовать список элементов в строку
        /// </summary>
        private static string AggregateIdsToString<TId>(IEnumerable<TId> ids)
            where TId : notnull =>
            String.Join(",", ids);
    }
}