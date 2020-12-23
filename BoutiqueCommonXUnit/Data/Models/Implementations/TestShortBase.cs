using System;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая базовая укороченная модель
    /// </summary>
    public abstract class TestShortBase: ITestShortBase
    {
        protected TestShortBase(TestEnum testEnum, string name)
        {
            TestEnum = testEnum;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public TestEnum Id => TestEnum;

        /// <summary>
        /// Тестовое перечисление
        /// </summary>
        public TestEnum TestEnum { get; }

        /// <summary>
        /// Тестовое поле
        /// </summary>
        public string Name { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ITestShortBase testEntity && Equals(testEntity);

        public bool Equals(ITestShortBase? other) =>
            other?.TestEnum == TestEnum &&
            other?.Name == Name;

        public override int GetHashCode() => HashCode.Combine(TestEnum, Name);
        #endregion
    }
}