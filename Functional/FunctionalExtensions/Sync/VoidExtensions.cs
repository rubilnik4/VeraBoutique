using System;

namespace Functional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для действий
    /// </summary>
    public static class VoidExtensions
    {
        /// <summary>
        /// Выполнить действие, вернуть тот же тип
        /// </summary>       
        public static TValue Void<TValue>(this TValue @this, Action<TValue> action)
        {
            action.Invoke(@this);
            return @this;
        }

        /// <summary>
        /// Выполнить действие при положительном условии
        /// </summary>
        public static TValue VoidOk<TValue>(this TValue @this, Func<TValue, bool> predicate, Action<TValue> action) =>
            predicate(@this)
                ? @this.Void(_ => action.Invoke(@this))
                : @this;
    }
}