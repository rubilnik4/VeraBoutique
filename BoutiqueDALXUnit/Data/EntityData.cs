using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDALXUnit.Data
{
    /// <summary>
    /// Тестовые данные сущностей
    /// </summary>
    public static class EntityData
    {
        /// <summary>
        /// Получить сущности типа пола
        /// </summary>
        public static List<GenderEntity> GetGenderEntities() =>
            GenderData.GetGendersDomain().
            Select(genderDomain => new GenderEntity(genderDomain.GenderType, genderDomain.Name)).
            ToList();

        /// <summary>
        /// Получить сущности для теста
        /// </summary>
        public static List<TestEntity> GetTestEntities() =>
            TestData.GetTestDomains().
            Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name)).
            ToList();

        /// <summary>
        /// Получить сущности для теста с включением
        /// </summary>
        public static List<TestEntity> GetTestEntitiesWithIncludes() =>
            TestData.GetTestDomains().
            Select(testDomain => new TestEntity(testDomain.TestEnum, testDomain.Name, TestIncludeEntities)).
            ToList();

        /// <summary>
        /// Тестовые сущности в результирующей коллекции
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntities =>
            new ResultCollection<TestEntity>(GetTestEntities());

        /// <summary>
        /// Пустая коллекция результирующих сущностей
        /// </summary>
        public static IResultCollection<TestEntity> TestResultEntitiesEmpty =>
           new ResultCollection<TestEntity>(Enumerable.Empty<TestEntity>());

        /// <summary>
        /// Тестовые сущности для включения в запрос
        /// </summary>
        private static IReadOnlyCollection<TestIncludeEntity> TestIncludeEntities =>
            new List<TestIncludeEntity>
            {
                new TestIncludeEntity("FirstInclude"),
                new TestIncludeEntity("SecondInclude"),
            };
    }
}