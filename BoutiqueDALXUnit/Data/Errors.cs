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
        public static IErrorResult NotFoundError => DatabaseErrors.ValueNotFoundError(String.Empty, String.Empty);

        /// <summary>
        /// Ошибка дублирующего элемента
        /// </summary>
        public static IErrorResult GetDuplicateError<TId>(TId id) =>
            DatabaseErrors.DuplicateError(id, String.Empty);

        /// <summary>
        /// Ошибка дублирующего элемента
        /// </summary>
        public static IErrorResult GetDuplicateError<TId>(IEnumerable<TId> ids) => 
            DatabaseErrors.DuplicateErrors(ids, String.Empty);
    }
}