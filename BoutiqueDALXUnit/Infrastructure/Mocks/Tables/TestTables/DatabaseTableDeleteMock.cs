using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables
{
    /// <summary>
    /// Таблицы баз данных. Удаление
    /// </summary>
    public static class DatabaseTableDeleteMock
    {
        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static ITestTable GetTestDatabaseTable(IEnumerable<TestEntity> testEntities) =>
             new TestTable(TestDatabaseSetMock.GetDbSetTest(testEntities).Object);

        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultValue<TestEntity> findEntity) =>
            GetTestDatabaseTable(DeleteOkFunc(), findEntity);

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(Func<TestEntity, IResultValue<TestEntity>> deleteFunc,
                                                            IResultValue<TestEntity> findEntity) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.Remove(It.IsAny<TestEntity>())).
                                        Returns(deleteFunc)).
            Void(tableMock => tableMock.Setup(table => table.FindExpressionAsync(It.IsAny<Func<IQueryable<TestEntity>, Task<TestEntity?>>>(),
                                                                                 It.IsAny<TestEnum>())).
                                        ReturnsAsync(findEntity));

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable(IResultError resultDelete) =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.Remove()).Returns(resultDelete));

        /// <summary>
        /// Функция удаления
        /// </summary>
        public static Func<TestEntity, IResultValue<TestEntity>> DeleteOkFunc() =>
             entity => new ResultValue<TestEntity>(entity);

        /// <summary>
        /// Функция удаления с ошибкой
        /// </summary>
        public static IResultValue<TestEntity> DeleteError =>
             new ResultValue<TestEntity>(ErrorData.DatabaseErrorType);

        /// <summary>
        /// Функция получения по идентификатору. Не найдено
        /// </summary>
        public static IResultValue<TestEntity> FirstNotFound =>
             new ResultValue<TestEntity>(Errors.NotFoundErrorType);
    }
}