using System;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая базовая модель
    /// </summary>
    public class Test: ITest, IEquatable<ITest>
    {
        public Test(TestEnum testEnum, string name)
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
        public override bool Equals(object? obj) => obj is ITest testEntity && Equals(testEntity);

        public bool Equals(ITest? other) =>
            other?.TestEnum == TestEnum &&
            other?.Name == Name;

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => HashCode.Combine(TestEnum, Name);
        #endregion
    }
}