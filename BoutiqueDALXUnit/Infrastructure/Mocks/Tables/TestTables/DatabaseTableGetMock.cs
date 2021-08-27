using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables
{
    /// <summary>
    /// Таблицы баз данных. Получение
    /// </summary>
    public static class DatabaseTableGetMock
    {
        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public static ITestTable GetTestDatabaseTable(IEnumerable<TestEntity> testEntities) =>
            new TestTable(TestDatabaseSetMock.GetDbSetTest(testEntities).Object);

        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities) =>
            GetTestDatabaseTable(testEntities, FindIdOkFunc(testEntities), FindIdsOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу в исполнении с функцией поиска элемента
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                            Func<TestEnum, IResultValue<TestEntity>> firstFunc) =>
            GetTestDatabaseTable(testEntities, firstFunc, FindIdsOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу в исполнении с функцией поиска элементов
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                            Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> findFunc) =>
            GetTestDatabaseTable(testEntities, FindIdOkFunc(testEntities), findFunc);


        /// <summary>
        /// Функция получения по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<TestEntity>> FindIdOkFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultCollectionOkToValue(entities => SearchInEntities.FirstEntity(entities, id));

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        public static Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> FindIdsOkFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultCollectionOk(entities => SearchInEntities.FindEntities(entities, id));

        /// <summary>
        /// Функция получения по идентификатору. Не найдено
        /// </summary>
        public static Func<TestEnum, IResultValue<TestEntity>> FirstNotFoundFunc(IResultCollection<TestEntity> entitiesResult) =>
            _ => entitiesResult.ResultValueBindOk(_ => new ResultValue<TestEntity>(DatabaseErrorData.NotFoundError));

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        private static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                             Func<TestEnum, IResultValue<TestEntity>> findIdFunc,
                                                             Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> findIdsFunc) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.ToListMainAsync()).ReturnsAsync(testEntities)).
            Void(tableMock => tableMock.Setup(table => table.FindMainByIdAsync(It.IsAny<TestEnum>())).
                                        ReturnsAsync((TestEnum id) => testEntities.ToResultValue().ResultValueBindOk(_ => findIdFunc(id)))).
            Void(tableMock => tableMock.Setup(table => table.FindMainByIdsAsync(It.IsAny<IEnumerable<TestEnum>>())).
                                        ReturnsAsync((IEnumerable<TestEnum> ids) => testEntities.ResultCollectionBindOk(_ => findIdsFunc(ids))));
    }
}