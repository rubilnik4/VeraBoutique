using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTable
{
    /// <summary>
    /// Таблицы баз данных. Отправка
    /// </summary>
    public static class DatabaseTablePostMock
    {
        /// <summary>
        /// Получить идентификатор по добавляемым сущностям
        /// </summary>
        public static IResultValue<TestEnum> AddIdOk(TestEntity entity) =>
           new ResultValue<TestEnum>(entity.Id);

        /// <summary>
        /// Получить идентификаторы по добавляемым сущностям
        /// </summary>
        public static IResultCollection<TestEnum> AddRangeIdOk(IEnumerable<TestEntity> entities) =>
            entities.Select(entity => entity.Id).
                     Map(ids => new ResultCollection<TestEnum>(ids));

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        public static Mock<ITestTable> GetTestDatabaseTable() =>
            new Mock<ITestTable>().
            Void(tableMock => tableMock.Setup(table => table.AddAsync(It.IsAny<TestEntity>())).
                                        ReturnsAsync((TestEntity entity) => AddIdOk(entity))).
            Void(tableMock => tableMock.Setup(table => table.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>())).
                                        ReturnsAsync((IEnumerable<TestEntity> entities) => AddRangeIdOk(entities)));
    }
}