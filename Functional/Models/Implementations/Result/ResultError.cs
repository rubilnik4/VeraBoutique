﻿using System.Collections.Generic;
using System.Linq;
using Functional.FunctionalExtensions;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.Result
{
    /// <summary>
    /// Базовый вариант ответа
    /// </summary>
    public class ResultError : IResultError
    {
        public ResultError()
            : this(Enumerable.Empty<IErrorResult>())
        { }

        public ResultError(IErrorResult error)
           : this(error.AsEnumerable()) { }

        public ResultError(IEnumerable<IErrorResult> errors)
        {
            Errors = errors.ToList();
        }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public IReadOnlyList<IErrorResult> Errors { get; }

        /// <summary>
        /// Присутствуют ли ошибки
        /// </summary>
        public bool HasErrors => Errors.Any();

        /// <summary>
        /// Отсутствие ошибок
        /// </summary>
        public bool OkStatus => !HasErrors;

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultError ConcatErrors(IEnumerable<IErrorResult> errors) =>
            new ResultError(Errors.Union(errors));

        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>
        public IResultValue<TValue> ToResultValue<TValue>(TValue value) where TValue : notnull =>
            OkStatus 
                ? new ResultValue<TValue>(value) 
                : new ResultValue<TValue>(Errors);
    }
}