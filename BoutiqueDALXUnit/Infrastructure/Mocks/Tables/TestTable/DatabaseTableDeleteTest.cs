using System;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
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
    /// Таблицы баз данных. Удаление
    /// </summary>
    public static class DatabaseTableDeleteTest
    {
        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities) =>
            GetTestDatabaseTable(testEntities, DeleteOk, DeleteOkFunc(), DatabaseTableGetMock.FindIdOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities, 
                                                            Func<TestEnum, IResultValue<TestEntity>> findIdFunc) =>
            GetTestDatabaseTable(testEntities, DeleteOk, DeleteOkFunc(), findIdFunc);

        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                              IResultError resultDelete) =>
            GetTestDatabaseTable(testEntities, resultDelete, DeleteOkFunc(), DatabaseTableGetMock.FindIdOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities, 
                                                            Func<TestEntity, IResultValue<TestEntity>> deleteFunc) =>
            GetTestDatabaseTable(testEntities, DeleteOk, deleteFunc, DatabaseTableGetMock.FindIdOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                            IResultError resultDelete,
                                                            Func<TestEntity, IResultValue<TestEntity>> deleteFunc,
                                                            Func<TestEnum, IResultValue<TestEntity>> findIdFunc) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.Remove(It.IsAny<TestEntity>())).
                                        Returns(deleteFunc)).
            Void(tableMock => tableMock.Setup(table => table.Remove()).
                                        Returns(resultDelete)).
            Void(tableMock => tableMock.Setup(table => table.FindShortIdAsync(It.IsAny<TestEnum>())).
                  
            ReturnsAsync((TestEnum id) => testEntities.ToResultValue().ResultValueBindOk(_ => findIdFunc(id))));

        /// <summary>
        /// Удаление
        /// </summary>
        public static IResultError DeleteOk =>
            new ResultError();

        /// <summary>
        /// Функция удаления
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEntity>> DeleteOkFunc() =>
             entity => new ResultValue<TestEntity>(entity);

        /// <summary>
        /// Функция удаления с ошибкой
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEntity>> DeleteErrorFunc() =>
             _ => new ResultValue<TestEntity>(ErrorData.DatabaseError);
    }
}