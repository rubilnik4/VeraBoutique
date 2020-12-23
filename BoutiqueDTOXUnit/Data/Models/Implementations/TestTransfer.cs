using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Models.Interfaces;

namespace BoutiqueDTOXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая трансферная модель
    /// </summary>
    public class TestTransfer : TestBase<TestIncludeTransfer>, ITestTransfer
    {
        public TestTransfer(ITestShortBase testShort, IEnumerable<TestIncludeTransfer> testIncludes)
           : this(testShort.TestEnum, testShort.Name, testIncludes)
        { }

        public TestTransfer(TestEnum testEnum, string name, IEnumerable<TestIncludeTransfer> testIncludes)
            : base(testEnum, name, testIncludes)
        { }
    }
}