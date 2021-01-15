using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;

namespace BoutiqueDTOXUnit.Data.Transfers
{
    /// <summary>
    /// Тестовые трансферные модели
    /// </summary>
    public static class TestTransferData
    {
        /// <summary>
        /// Тестовые трансферные модели
        /// </summary>
        public static IReadOnlyCollection<TestTransfer> TestTransfers =>
            TestData.TestDomains.
            Select(test =>
                new TestTransfer(test, test.TestIncludes.Select(testInclude => new TestIncludeTransfer(testInclude)))).
            ToList();
    }
}