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
    public class TestTransfer : TestShortTransfer, ITestTransfer
    {
        public TestTransfer()
        { }

        public TestTransfer(ITest test, IEnumerable<TestIncludeTransfer> testIncludes)
           : this(test.TestEnum, test.Name, testIncludes)
        { }

        public TestTransfer(TestEnum testEnum, string name, IEnumerable<TestIncludeTransfer> testIncludes)
            : base(testEnum, name)
        {
            TestIncludes = testIncludes.ToList();
        }

        /// <summary>
        /// Включенные сущности
        /// </summary>
        public IReadOnlyCollection<TestIncludeTransfer> TestIncludes { get; set; } = null!;
    }
}