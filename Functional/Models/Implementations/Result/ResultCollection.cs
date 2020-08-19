using System;
using Functional.Models.Interfaces.Result;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Functional.FunctionalExtensions.Sync.ResultExtension;

namespace Functional.Models.Implementations.Result
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией
    /// </summary>
    public class ResultCollection<TValue> : ResultValue<IReadOnlyCollection<TValue>>, IResultCollection<TValue>
    {
        public ResultCollection(IErrorResult error)
            : this(error.AsEnumerable()) { }

        public ResultCollection(IEnumerable<IErrorResult> errors)
            : this(Enumerable.Empty<TValue>(), errors)
        { }

        public ResultCollection(IEnumerable<TValue> valueCollection)
            : this(valueCollection, Enumerable.Empty<IErrorResult>())
        { }

        protected ResultCollection([AllowNull] IEnumerable<TValue> valueCollection, IEnumerable<IErrorResult> errors)
            : base(valueCollection.ToList().AsReadOnly(), errors)
        { }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public new IResultCollection<TValue> ConcatErrors(IEnumerable<IErrorResult> errors) =>
            base.ConcatErrors(errors).ToResultCollection();
    }
}