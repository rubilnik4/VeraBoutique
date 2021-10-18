using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Extensions.TaskExtensions
{
    /// <summary>
    /// Методы расширения для задач
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Выполнить все асинхронные функции и дождаться последней
        /// </summary>
        public static async Task<IList<TValue>> WaitAll<TValue>(this IEnumerable<Task<TValue>> @this) =>
            await Task.WhenAll(@this);

        /// <summary>
        /// Обработать список задач в асинхронной последовательности
        /// </summary>
        public static async Task<IReadOnlyCollection<TValueOut>> SelectAsync<TValueIn, TValueOut>(this IEnumerable<TValueIn> @this,
                                                                                                  Func<TValueIn, Task<TValueOut>> func)
        {
            var awaitedItems = new List<TValueOut>();
            var collection = @this.ToList();
            foreach (var item in collection)
            {
                var awaitedItem = await func(item);
                awaitedItems.Add(awaitedItem);
            }
            return awaitedItems;
        }

        /// <summary>
        /// Выполнить все асинхронные действия и дождаться последней
        /// </summary>
        public static async Task WaitAll(this IEnumerable<Task> @this) =>
            await Task.WhenAll(@this);

        /// <summary>
        /// Преобразовать множество в задачу
        /// </summary>
        public static Task<IEnumerable<TValue>> ToTaskEnumerable<TValue>(IEnumerable<TValue> collection) =>
            Task.FromResult(collection);
    }
}