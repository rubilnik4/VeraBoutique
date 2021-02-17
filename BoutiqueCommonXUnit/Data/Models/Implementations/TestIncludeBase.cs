using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая вложенная базовая модель
    /// </summary>
    public class TestIncludeBase : ITestIncludeBase
    {
        public TestIncludeBase(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ITestIncludeBase testIncludeEntity && Equals(testIncludeEntity);

        public bool Equals(ITestIncludeBase? other) =>
            other?.Name == Name ;

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => HashCode.Combine( Name);
        #endregion

        /// <summary>
        /// Получить хэш-код группы
        /// </summary>
        public static double GetTestIncludeHashCodes<TInclude>(IEnumerable<TInclude> testIncludes)
            where TInclude : ITestIncludeBase =>
            testIncludes.Average(testInclude => testInclude.GetHashCode());
    }
}