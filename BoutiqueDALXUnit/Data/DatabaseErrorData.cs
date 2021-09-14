using System;
using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые ошибки
    /// </summary>
    public static class DatabaseErrorData
    {
        /// <summary>
        /// Ошибка ненайденного элемента
        /// </summary>
        public static IErrorResult NotFoundError => 
            DatabaseErrors.ValueNotFoundError(String.Empty, String.Empty);

        /// <summary>
        /// Ошибка ненайденного элемента
        /// </summary>
        public static IErrorResult TableError =>
            DatabaseErrors.AccessError("TestTable");

        /// <summary>
        /// Ошибка дублирующего элемента
        /// </summary>
        public static IErrorResult GetDuplicateError<TId>(TId id)
            where TId : notnull =>
            DatabaseErrors.DuplicateError(id, String.Empty);

        /// <summary>
        /// Ошибка дублирующего элемента
        /// </summary>
        public static IErrorResult GetDuplicateError<TId>(IEnumerable<TId> ids)
            where TId : notnull =>
            DatabaseErrors.DuplicateErrors(ids, String.Empty);
    }
}