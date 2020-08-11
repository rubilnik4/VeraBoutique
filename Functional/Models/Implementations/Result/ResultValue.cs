using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Functional.FunctionalExtensions;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.Result
{
    /// <summary>
    /// Базовый вариант ответа со значением
    /// </summary>
    public class ResultValue<TValue> : IResultValue<TValue> where TValue : notnull
    {
        public ResultValue(IErrorResult error)
            : this(error.AsEnumerable()) { }

        public ResultValue(IEnumerable<IErrorResult> errors)
        {
            var errorCollection = errors?.ToList() ?? throw new ArgumentNullException(nameof(errors));
            if (!ValidateCollection(errorCollection)) throw new NullReferenceException(nameof(errors));

            Value = default;
            Errors = errorCollection;
        }

        public ResultValue(TValue value, IErrorResult? errorNull = null)
          : this(value, Enumerable.Empty<IErrorResult>(), errorNull) { }

        public ResultValue(TValue value, IEnumerable<IErrorResult> errors, IErrorResult? errorNull = null)
            : this(errors)
        {
            Errors = value switch
            {
                null when errorNull != null => Errors.Concat(errorNull).ToList(),
                null => throw new ArgumentNullException(nameof(value)),
                _ => Errors
            };

            Value = value;
        }

        /// <summary>
        /// Список значений
        /// </summary>
        [MaybeNull]
        public TValue Value { get; protected set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public IReadOnlyList<IErrorResult> Errors { get; protected set; }

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
        public IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors) =>
            errors != null ?
            Value.WhereContinue(value => value != null,
                okFunc: value => new ResultValue<TValue>(value, Errors.Union(errors)),
                badFunc: value => new ResultValue<TValue>(Errors.Union(errors))) :
            this;

        /// <summary>
        /// Преобразовать в результирующий тип
        /// </summary>
        public IResultError ToResult() => new ResultError(Errors);

        /// <summary>
        /// Проверить ошибки на корректность
        /// </summary>      
        protected static bool ValidateCollection<T>(IEnumerable<T> collection) =>
            collection?.All(t => t != null) == true;
    }
}