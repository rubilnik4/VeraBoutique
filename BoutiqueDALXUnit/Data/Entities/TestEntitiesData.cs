using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data.Entities
{
    /// <summary>
    /// Данные тестовых сущностей
    /// </summary>
    public static class TestEntitiesData
    {
        /// <summary>
        /// Получить сущности для теста
        /// </summary>
        public static IReadOnlyCollection<TestEntity> TestEntities =>
            TestData.TestDomains.
            Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name,
                                                testDomain.TestIncludes.Select(test => new TestIncludeEntity(test.Name)))).
            ToList();

        /// <summary>
        /// Тестовые сущности в результирующей коллекции
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntities =>
            new ResultCollection<TestEntity>(TestEntities);

        /// <summary>
        /// Пустая коллекция результирующих сущностей
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntitiesEmpty =>
            new ResultCollection<TestEntity>(Enumerable.Empty<TestEntity>());
    }
}