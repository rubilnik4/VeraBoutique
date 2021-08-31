﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением
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
            if (value == null && !Errors.Any()) throw new ArgumentNullException(nameof(errors));

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
            base.ConcatErrors(errors).
            ToResultValue(Value);
    }
}