using System;

namespace Functional.FunctionalExtensions
{
    /// <summary>
    /// Методы расширения для преобразования типов
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Преобразование типов с помощью функции
        /// </summary>       
        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> func) =>
            func(@this);

        /// <summary>
        /// Выполнить действие, вернуть тот же тип
        /// </summary>       
        public static T Void<T>(this T @this, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            action.Invoke(@this);
            return @this;
        }
    }
}
