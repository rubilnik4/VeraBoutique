using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
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
        public static Mock<ITestTable> GetTestDatabaseTable(Func<TestEntity, IResultValue<TestEntity>> addFunc) =>
            GetTestDatabaseTable(addFunc, AddRangeIdOk());

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<IEnumerable<TestEntity>, IResultCollection<TestEntity>> addRangeFunc) =>
            GetTestDatabaseTable(AddIdOk(), addRangeFunc);

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<TestEntity, IResultValue<TestEntity>> addFunc,
                                                            Func<IEnumerable<TestEntity>, IResultCollection<TestEntity>> addRangeFunc) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.AddAsync(It.IsAny<TestEntity>())).
                                        ReturnsAsync(addFunc)).
            Void(tableMock => tableMock.Setup(table => table.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>())).
                                        ReturnsAsync(addRangeFunc));

        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEntity>> AddIdOk() =>
           entity => new ResultValue<TestEntity>(entity);

        /// <summary>
        /// Получить идентификаторы по добавляемым сущностям
        /// </summary>
        public static Func<IEnumerable<TestEntity>, IResultCollection<TestEntity>> AddRangeIdOk() =>
            entities => new ResultCollection<TestEntity>(entities);

        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEntity>> AddIdError() =>
           _ => new ResultValue<TestEntity>(DatabaseErrorData.TableError);

        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static Func<IEnumerable<TestEntity>, IResultCollection<TestEntity>> AddRangeIdError() =>
           _ => new ResultCollection<TestEntity>(DatabaseErrorData.TableError);
    }
}