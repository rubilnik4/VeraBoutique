using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Functional.FunctionalExtensions;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.Result
{
    /// <summary>
    /// Базовый вариант ответа со значением типа класс
    /// </summary>
    public class ResultValue<TValue> : ResultError, IResultValue<TValue>
    {
        public ResultValue(IErrorResult error)
            : this(error.AsEnumerable()) { }

        public ResultValue(IEnumerable<IErrorResult> errors)
            : this(default, errors)
        { }

        public ResultValue(TValue value)
            : this(value, Enumerable.Empty<IErrorResult>())
        { }

        protected ResultValue([AllowNull] TValue value, IEnumerable<IErrorResult> errors)
            : base(errors)
        {
            Value = value;
        }

        /// <summary>
        /// Значение
        /// </summary>
        [AllowNull]
        public TValue Value { get; }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors) =>
            new ResultValue<TValue>(Value, base.ConcatErrors(errors).Errors);
    }
}