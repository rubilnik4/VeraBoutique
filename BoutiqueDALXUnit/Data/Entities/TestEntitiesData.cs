using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Implementations.Result;
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
        public static List<TestEntity> TestEntities =>
            TestData.GetTestDomains().
                     Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name)).
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