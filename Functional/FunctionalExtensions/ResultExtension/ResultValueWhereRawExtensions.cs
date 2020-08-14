using System;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.ResultExtension
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением
    /// </summary>
    public static class ResultValueWhereRawExtensions
    {
        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе в обертке
        /// </summary>   
        public static IResultValue<TValueOut> ResultValueOkRaw<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<IResultValue<TValueIn>, IResultValue<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this)
                : new ResultValue<TValueOut>(@this.Errors);
    }
}