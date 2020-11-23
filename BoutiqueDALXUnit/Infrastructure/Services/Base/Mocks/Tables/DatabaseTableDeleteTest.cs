using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.Tables
{
    /// <summary>
    /// Таблицы баз данных. Удаление
    /// </summary>
    public static class DatabaseTableDeleteTest
    {
        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities) =>
            GetTestDatabaseTable(testEntities, DatabaseTableGetMock.FindIdOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                             Func<TestEnum, IResultValue<TestEntity>> findIdFunc) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.Remove(It.IsAny<TestEntity>())).
                                        Returns((TestEntity entity) => new ResultValue<TestEntity>(entity))).
            Void(tableMock => tableMock.Setup(table => table.FindIdAsync(It.IsAny<TestEnum>())).
                                        ReturnsAsync((TestEnum id) => testEntities.ToResultValue().ResultValueBindOk(_ => findIdFunc(id))));
    }
}