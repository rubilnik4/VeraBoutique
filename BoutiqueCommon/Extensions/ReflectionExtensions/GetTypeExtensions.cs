using System;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueCommon.Extensions.ReflectionExtensions
{
    /// <summary>
    /// Методы расширения для типов данных
    /// </summary>
    public static class GetTypeExtensions
    {
        /// <summary>
        /// Исключить обобщения из типа данных
        /// </summary>
        public static string GetNameWithoutGenerics(this Type type) =>
            type.Name.
            Map(typeName => (TypeName: typeName, Index: typeName.IndexOf('`'))).
            WhereContinue(nameIndex => nameIndex.Index > -1,
                          nameIndex => nameIndex.TypeName.Substring(0, nameIndex.Index),
                          nameIndex => nameIndex.TypeName);
    }
}