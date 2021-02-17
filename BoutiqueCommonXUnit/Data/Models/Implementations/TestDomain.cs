using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using Xunit.Abstractions;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    public class TestDomain: TestBase<ITestIncludeDomain>, ITestDomain
    {
        public TestDomain(ITestShortBase testShort, IEnumerable<ITestIncludeDomain> testIncludes)
           : this(testShort.TestEnum, testShort.Name, testIncludes)
        { }

        public TestDomain(TestEnum testEnum, string name, IEnumerable<ITestIncludeDomain> testIncludes)
            :base(testEnum, name, testIncludes)
        { }
    }
}