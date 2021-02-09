using System;
using System.Linq;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка
    /// </summary>
    public static class ResultValueCurryExtensions
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента
        /// </summary>
        public static IResultValue<Func<TOut>> ResultCurryBindOk<TIn1, TOut>(this IResultValue<Func<TIn1, TOut>> @this,
                                                                             IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов
        /// </summary>
        public static IResultValue<Func<TIn2, TOut>> ResultCurryBindOk<TIn1, TIn2, TOut>(this IResultValue<Func<TIn1, TIn2, TOut>> @this,
                                                                                         IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов
        /// </summary>
        public static IResultValue<Func<TIn2, TIn3, TOut>> ResultCurryBindOk<TIn1, TIn2, TIn3, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TOut>> @this,
                                                                                                     IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TIn3, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TIn3, TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов
        /// </summary>
        public static IResultValue<Func<TIn2, TIn3, TIn4, TOut>> ResultCurryBindOk<TIn1, TIn2, TIn3, TIn4, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                 IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TIn3, TIn4, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TIn3, TIn4, TOut>>(@this.Errors.Concat(arg1.Errors));

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов
        /// </summary>
        public static IResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>> ResultCurryBindOk<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IResultValue<Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                             IResultValue<TIn1> arg1) =>
            @this.OkStatus && arg1.OkStatus
                ? new ResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>(@this.Value.Curry(arg1.Value))
                : new ResultValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>(@this.Errors.Concat(arg1.Errors));
    }
}