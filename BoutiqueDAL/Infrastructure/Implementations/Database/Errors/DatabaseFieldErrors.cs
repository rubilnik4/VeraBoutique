using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;

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
        public static IErrorResult FieldNotValid<TId, TDomain, TValue>(TValue value, string tableName)
            where TDomain : IDomainModel<TId>
            where TId : notnull
            where TValue : notnull =>
            FieldNotValid<TId, TDomain, TValue>(value, tableName, "Некорректное значение");
        
        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain, TValue>(int min, TValue value, string tableName)
            where TDomain : IDomainModel<TId>
            where TId : notnull
            where TValue : notnull =>
            FieldNotValid<TId, TDomain, TValue>(value, tableName, $"Меньше допустимого значения {min}");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain, TValue>(int min, int max, TValue value, string tableName)
            where TDomain : IDomainModel<TId>
            where TId : notnull
            where TValue : notnull =>
            FieldNotValid<TId, TDomain, TValue>(value, tableName, $"Превышает допустимые пределы от {min} до {max}");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain, TValue>(TValue value, string tableName, string description)
            where TDomain : IDomainModel<TId>
            where TId : notnull
            where TValue : notnull =>
            ErrorResultFactory.DatabaseValueNotValidError(value, tableName, $"{description} {value.GetType()} в [{typeof(TDomain)}]");
    }
}