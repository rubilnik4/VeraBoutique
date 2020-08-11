using System;

namespace Functional.FunctionalExtensions
{
    /// <summary>
    /// Методы расширения для действий
    /// </summary>
    public static class VoidExtensions
    {
        /// <summary>
        /// Выполнить действие, вернуть тот же тип
        /// </summary>       
        public static T Void<T>(this T @this, Action<T> action)
        {
            action.Invoke(@this);
            return @this;
        }
    }
}