using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

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
        public static List<ITestDomain> TestDomains =>
            new List<ITestDomain>()
            {
                new TestDomain(TestEnum.First, "First", TestIncludeDomains ),
                new TestDomain(TestEnum.Second, "Second", TestIncludeDomains),
            };

        /// <summary>
        /// Получить тестовые вложенные модели
        /// </summary>
        public static List<ITestIncludeDomain> TestIncludeDomains =>
            new List<ITestIncludeDomain>()
            {
                new TestIncludeDomain("FirstInclude"),
                new TestIncludeDomain("SecondInclude"),
            };

        /// <summary>
        /// Получить идентификаторы
        /// </summary>
        public static IReadOnlyCollection<TestEnum> GetTestIds(IEnumerable<ITestDomain> testDomains) =>
            testDomains.Select(test => test.Id).ToList().AsReadOnly();

        /// <summary>
        /// Тестовая сущность в результирующем значении
        /// </summary>
        public static IResultValue<ITestDomain> TestResultDomain =>
            new ResultValue<ITestDomain>(TestDomains.First());

        /// <summary>
        /// Тестовые сущности в результирующей коллекции
        /// </summary>
        public static IResultCollection<ITestDomain> TestResultDomains =>
            new ResultCollection<ITestDomain>(TestDomains);
    }
}