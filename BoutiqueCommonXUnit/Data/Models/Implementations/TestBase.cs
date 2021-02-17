using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая базовая модель
    /// </summary>
    public abstract class TestBase<TInclude> : TestShortBase, ITestBase<TInclude>
        where TInclude : ITestIncludeBase
    {
        protected TestBase(TestEnum testEnum, string name, IEnumerable<TInclude> testIncludes)
            :base(testEnum, name)
        {
            TestIncludes = testIncludes.ToList();
        }

        /// <summary>
        /// Вложения
        /// </summary>
        public IReadOnlyCollection<TInclude> TestIncludes { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ITestBase<TInclude> testEntity && Equals(testEntity);

        public bool Equals(ITestBase<TInclude>? other) =>
            base.Equals(other) &&
            other?.TestIncludes.SequenceEqual(TestIncludes) == true;

        public override int GetHashCode() => HashCode.Combine(TestEnum, Name, TestIncludeBase.GetTestIncludeHashCodes(TestIncludes));
        #endregion
    }
}