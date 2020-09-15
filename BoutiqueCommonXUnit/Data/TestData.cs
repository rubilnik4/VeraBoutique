using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Тестовые данные
    /// </summary>
    public static class TestData
    {
        /// <summary>
        /// Получить тестовые модели
        /// </summary>
        public static List<ITestDomain> GetTestDomains() =>
            new List<ITestDomain>()
            {
                new TestDomain(TestEnum.First, "First" ),
                new TestDomain(TestEnum.Second, "Second"),
            };

        /// <summary>
        /// Получить идентификаторы
        /// </summary>
        public static IReadOnlyCollection<TestEnum> GetTestIds(IEnumerable<ITestDomain> testDomains) =>
            testDomains.Select(test => test.Id).ToList().AsReadOnly();
    }
}