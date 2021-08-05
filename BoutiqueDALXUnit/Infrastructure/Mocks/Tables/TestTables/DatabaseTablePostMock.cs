using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables
{
    /// <summary>
    /// Таблицы баз данных. Отправка
    /// </summary>
    public static class DatabaseTablePostMock
    {
        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static ITestTable GetTestDatabaseTable(IEnumerable<TestEntity> testEntities) =>
             new TestTable(TestDatabaseSetMock.GetDbSetTest(testEntities).Object);

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<TestEntity, IResultValue<TestEnum>> addIdFunc) =>
            GetTestDatabaseTable(addIdFunc, AddRangeIdOk());

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<IEnumerable<TestEntity>, IResultCollection<TestEnum>> addRangeIdFunc) =>
            GetTestDatabaseTable(AddIdOk(), addRangeIdFunc);

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<TestEntity, IResultValue<TestEnum>> addIdFunc,
                                                            Func<IEnumerable<TestEntity>, IResultCollection<TestEnum>> addRangeIdFunc) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.AddAsync(It.IsAny<TestEntity>())).
                                        ReturnsAsync(addIdFunc)).
            Void(tableMock => tableMock.Setup(table => table.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>())).
                                        ReturnsAsync(addRangeIdFunc));

        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEnum>> AddIdOk() =>
           entity => new ResultValue<TestEnum>(entity.Id);

        /// <summary>
        /// Получить идентификаторы по добавляемым сущностям
        /// </summary>
        public static Func<IEnumerable<TestEntity>, IResultCollection<TestEnum>> AddRangeIdOk() =>
            entities => entities.Select(entity => entity.Id).
                        Map(ids => new ResultCollection<TestEnum>(ids));

        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEnum>> AddIdError() =>
           _ => new ResultValue<TestEnum>(ErrorData.DatabaseError);

        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static Func<IEnumerable<TestEntity>, IResultCollection<TestEnum>> AddRangeIdError() =>
           _ => new ResultCollection<TestEnum>(ErrorData.DatabaseError);
    }
}