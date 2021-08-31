using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа
    /// </summary>
    public static class ResultValueAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IErrorResult> errorNull)
            where TValue : class =>
            @this.ToResultValueNullCheck(await errorNull);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IErrorResult> errorNull)
            where TValue : struct =>
            @this.ToResultValueNullCheck(await errorNull);
    }
}