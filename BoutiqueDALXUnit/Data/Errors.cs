using System;
using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые ошибки
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// Ошибка ненайденного элемента
        /// </summary>
        public static IErrorResult NotFoundErrorType => DatabaseErrors.ValueNotFoundError(String.Empty, String.Empty);

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