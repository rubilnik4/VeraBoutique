﻿using System;
using System.Collections.Generic;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией с возвращением к значению
    /// </summary>
    public static class ResultValueWhereToCollectionExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе коллекции с возвращением к значению
        /// </summary>      
        public static IResultValue<TValueOut> ResultCollectionContinueToValue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                       Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                       Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                       Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
            @this.ToResultValue().
            ResultValueContinue(predicate, okFunc, badFunc);

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе коллекции с возвращением к значению
        /// </summary>      
        public static IResultValue<TValueOut> ResultCollectionOkBadToValue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            @this.ToResultValue().
            ResultValueOkBad(okFunc, badFunc);

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе коллекции с возвращением к значению
        /// </summary>   
        public static IResultValue<TValueOut> ResultCollectionOkToValue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                             Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc) =>
            @this.ToResultValue().
            ResultValueOk(okFunc);
    }
}