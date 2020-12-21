using System;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Extensions.StringExtensions
{
    /// <summary>
    /// Методы расширения для строк
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Вырезать с строке подстроку
        /// </summary>
        public static string SubstringRemove(this string @this, string substring) =>
            @this.IndexOf(substring, StringComparison.InvariantCultureIgnoreCase).
            WhereContinue(index => index > -1,
                          index => @this.Remove(index, substring.Length),
                          _ => @this);
    }
}