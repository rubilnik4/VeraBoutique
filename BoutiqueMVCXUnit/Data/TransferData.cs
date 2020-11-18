using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Data.Models.Implementations;
using BoutiqueDTO.Data.Models.Interfaces;

namespace BoutiqueMVCXUnit.Data
{
    public class TransferData
    {
        /// <summary>
        /// Получить трансферные модели для теста
        /// </summary>
        public static IList<ITestTransfer> GetTestTransfers() =>
            TestData.TestDomains().
            Select(testDomain => (ITestTransfer)new TestTransfer(testDomain.TestEnum, testDomain.Name)).
            ToList();

        /// <summary>
        /// Получить трансферную модель для теста
        /// </summary>
        public static ITestTransfer GetTestTransfer() =>
            GetTestTransfers().First();

        /// <summary>
        /// Получить идентификаторы
        /// </summary>
        public static IReadOnlyCollection<TestEnum> GetTestIds(IEnumerable<ITestTransfer> testTransfers) =>
            testTransfers.Select(test => test.Id).ToList().AsReadOnly();
    }
}