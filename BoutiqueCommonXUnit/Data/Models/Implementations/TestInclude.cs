using System;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая вложенная базовая модель
    /// </summary>
    public class TestInclude : ITestInclude, IEquatable<ITestInclude>
    {
        public TestInclude(string name)
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
        public override bool Equals(object obj) => obj is ITestInclude testIncludeEntity && Equals(testIncludeEntity);

        public bool Equals(ITestInclude other) =>
            other?.Name== Name ;

        // ReSharper disable once NonReadonlyMemberInGetHashCode
        public override int GetHashCode() => HashCode.Combine( Name);
        #endregion
    }
}