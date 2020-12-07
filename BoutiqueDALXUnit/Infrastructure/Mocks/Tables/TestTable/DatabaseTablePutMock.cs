using System;
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
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.Update(It.IsAny<TestEntity>())).
                                        Returns((TestEntity entity) => new ResultError()));
    }
}