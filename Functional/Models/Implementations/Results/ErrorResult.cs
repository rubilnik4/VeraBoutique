using System;
using System.Collections;
using System.Collections.Generic;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.Results
{
    public abstract class ErrorResult : IErrorResult
    {
        protected ErrorResult(string description)
            : this(description, null) { }

        protected ErrorResult(string description, Exception? exception)
        {
            Description = description;
            Exception = exception;
        }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Исключение
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        public abstract bool HasErrorType<TError>()
            where TError : struct;

        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
        public abstract IErrorResult AppendException(Exception exception);

        /// <summary>
        /// Преобразовать в ответ
        /// </summary>      
        public IResultError ToResult() =>
            new ResultError(this);

        /// <summary>
        /// Преобразовать в ответ с вложенным типом
        /// </summary>      
        public IResultValue<TValue> ToResultValue<TValue>() =>
            new ResultValue<TValue>(this);

        /// <summary>
        /// Преобразовать в ответ с вложенной коллекцией
        /// </summary>      
        public IResultCollection<TValue> ToResultCollection<TValue>() =>
            new ResultCollection<TValue>(this);

        #region IEnumerable Support
        /// <summary>
        /// Реализация перечисления
        /// </summary>       
        public IEnumerator<IErrorResult> GetEnumerator()
        {
            yield return this;
        }

        /// <summary>
        /// Реализация перечисления
        /// </summary>  
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        #endregion
    }
}