using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    public class TestDomain: TestShortDomain, ITestDomain, IEquatable<ITestDomain>
    {
        public TestDomain(ITest test, IEnumerable<ITestIncludeDomain> testIncludes)
           : this(test.TestEnum, test.Name, testIncludes)
        { }

        public TestDomain(TestEnum testEnum, string name, IEnumerable<ITestIncludeDomain> testIncludes)
            :base(testEnum, name)
        {
            TestIncludes = testIncludes.ToList();
        }

        /// <summary>
        /// Включенные сущности
        /// </summary>
        public IReadOnlyCollection<ITestIncludeDomain> TestIncludes { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is ITestDomain testDomain && Equals(testDomain);

        public bool Equals(ITestDomain? other) =>
            other?.Id == Id &&
            other?.TestIncludes.SequenceEqual(TestIncludes) == true;

        public override int GetHashCode() => HashCode.Combine(TestEnum, Name, TestIncludes.Average(test => test.GetHashCode()));
        #endregion
    }
}