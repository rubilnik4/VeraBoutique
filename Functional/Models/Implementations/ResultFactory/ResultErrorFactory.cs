using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.Models.Implementations.ResultFactory
{
    /// <summary>
    /// Фабрика для создания результирующего ответа
    /// </summary>
    public static class ResultErrorFactory
    {
        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        public static Task<IResultError> CreateTaskResultError()=>
            Task.FromResult((IResultError)new ResultError());

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static Task<IResultError> CreateTaskResultError(IErrorResult error)=>
            Task.FromResult((IResultError)new ResultError(error));

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static Task<IResultError> CreateTaskResultError(IEnumerable<IErrorResult> errors)=>
            Task.FromResult((IResultError)new ResultError(errors));

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static Task<IResultError> CreateTaskResultError(IResultError error) =>
            Task.FromResult(error);

        /// <summary>
        /// Создать асинхронный результирующий ответ
        /// </summary>
        public static async Task<IResultError> CreateTaskResultErrorAsync() =>
            await Task.FromResult((IResultError)new ResultError());

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static async Task<IResultError> CreateTaskResultErrorAsync(IErrorResult error)=>
            await Task.FromResult((IResultError)new ResultError(error));

        /// <summary>
        /// Создать асинхронный результирующий ответ с ошибкой
        /// </summary>
        public static async Task<IResultError> CreateTaskResultErrorAsync(IEnumerable<IErrorResult> errors) =>
            await Task.FromResult((IResultError)new ResultError(errors));
    }
}