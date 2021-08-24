using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.ResultFactory
{
    /// <summary>
    /// Фабрика для создания результирующего ответа со значением
    /// </summary>
    public static class ResultValueFactory
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        public static Task<IResultValue<TValue>> CreateTaskResultValue<TValue>(TValue value)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(value));

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением и ошибкой
        /// </summary>
        public static Task<IResultValue<TValue>> CreateTaskResultValueError<TValue>(IErrorResult error)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(error));

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением и ошибкой
        /// </summary>
        public static Task<IResultValue<TValue>> CreateTaskResultValueError<TValue>(IEnumerable<IErrorResult> errors)
            where TValue : notnull =>
            Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(errors));

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> CreateTaskResultValueAsync<TValue>(TValue value)
            where TValue : notnull =>
            await Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(value));

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением и ошибкой
        /// </summary>
        public static async Task<IResultValue<TValue>> CreateTaskResultValueErrorAsync<TValue>(IErrorResult error)
            where TValue : notnull =>
            await Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(error));

        /// <summary>
        /// Создать асинхронный результирующий ответ со значением и ошибкой
        /// </summary>
        public static async Task<IResultValue<TValue>> CreateTaskResultValueErrorAsync<TValue>(IEnumerable<IErrorResult> errors)
            where TValue : notnull =>
            await Task.FromResult((IResultValue<TValue>)new ResultValue<TValue>(errors));
    }
}