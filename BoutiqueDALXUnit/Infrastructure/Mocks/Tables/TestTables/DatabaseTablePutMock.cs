using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Results;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables
{
    /// <summary>
    /// Таблицы баз данных. Обновление
    /// </summary>
    public static class DatabaseTablePutMock
    {
        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static ITestTable GetTestDatabaseTable(IEnumerable<TestEntity> testEntities) =>
             new TestTable(TestDatabaseSetMock.GetDbSetTest(testEntities).Object);

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable() =>
            GetTestDatabaseTable(UpdateOk());

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<TestEntity, ResultError> updateFunc) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.Update(It.IsAny<TestEntity>())).
                                        Returns(updateFunc));

        /// <summary>
        /// Функция обновления
        /// </summary>
        public static Func<TestEntity, ResultError> UpdateOk() =>
            _ => new ResultError();

        /// <summary>
        /// Функция обновления c ошибкой
        /// </summary>
        public static Func<TestEntity, ResultError> UpdateError() =>
            _ => new ResultError(ErrorData.DatabaseErrorType);
    }
}