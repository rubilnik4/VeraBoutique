using System;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTable
{
    /// <summary>
    /// Таблицы баз данных. Обновление
    /// </summary>
    public static class DatabaseTablePutMock
    {
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
            _ => new ResultError(ErrorData.DatabaseError);
    }
}