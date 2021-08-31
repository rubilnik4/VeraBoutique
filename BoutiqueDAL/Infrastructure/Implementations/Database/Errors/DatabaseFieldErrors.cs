using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Errors
{
    /// <summary>
    /// Ошибки в сущностях
    /// </summary>
    public static class DatabaseFieldErrors
    {
        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TValue>(TValue value, string tableName)
            where TValue : notnull =>
            FieldNotValid(value, tableName, "Некорректное значение");
        
        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldRangeNotValid<TValue>(int min, TValue value, string tableName)
            where TValue : notnull =>
            FieldNotValid(value, tableName, $"Меньше допустимого значения {min}");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldRangeNotValid<TValue>(int min, int max, TValue value, string tableName)
            where TValue : notnull =>
            FieldNotValid(value, tableName, $"Превышает допустимые пределы от {min} до {max}");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TValue>(TValue value, string tableName, string description)
            where TValue : notnull =>
            ErrorResultFactory.DatabaseValueNotValidError(value, tableName, $"{description} {value?.GetType()} в [{tableName}]");
    }
}