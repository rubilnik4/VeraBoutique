using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using static BoutiqueDALXUnit.Data.Services.Implementation.SearchInModels;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks
{
    /// <summary>
    /// 
    /// </summary>
    public static class DatabaseTableMock
    {
        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        public static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities) =>
            GetTestDatabaseTable(testEntities, FirstOkFunc(testEntities), FindOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу в исполнении с функцией поиска элемента
        /// </summary>
        public static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                                     Func<TestEnum, IResultValue<TestEntity>> firstFunc) =>
            GetTestDatabaseTable(testEntities, firstFunc, FindOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу в исполнении с функцией поиска элементов
        /// </summary>
        public static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                                    Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> findFunc) =>
            GetTestDatabaseTable(testEntities, FirstOkFunc(testEntities), findFunc);

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        private static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                                     Func<TestEnum, IResultValue<TestEntity>> firstFunc,
                                                                     Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> findFunc) =>
            new Mock<ITestDatabaseTable>().
            Void(tableMock => tableMock.Setup(table => table.ToListAsync()).ReturnsAsync(testEntities)).
            Void(tableMock => tableMock.Setup(table => table.FindAsync(It.IsAny<TestEnum>())).
                                        ReturnsAsync((TestEnum id) => testEntities.ToResultValue().ResultValueBindOk(_ => firstFunc(id)))).
            Void(tableMock => tableMock.Setup(table => table.FindAsync(It.IsAny<IEnumerable<TestEnum>>())).
                                        ReturnsAsync((IEnumerable<TestEnum> ids) => testEntities.ResultCollectionBindOk(_ => findFunc(ids)))).
            Void(tableMock => tableMock.Setup(table => table.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>())).
                                        ReturnsAsync((IEnumerable<TestEntity> entities) => AddRangeIdOk(entities))).
            Void(tableMock => tableMock.Setup(table => table.Update(It.IsAny<TestEntity>())).
                                        Returns((TestEntity entity) => new ResultError())).
            Void(tableMock => tableMock.Setup(table => table.Remove(It.IsAny<TestEntity>())).
                                        Returns((TestEntity entity) => new ResultValue<TestEntity>(entity)));

        /// <summary>
        /// Функция получения по идентификатору
        /// </summary>
        public static Func<TestEnum, IResultValue<TestEntity>> FirstOkFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultCollectionOkToValue(entities => FirstEntity(entities, id));

        /// <summary>
        /// Функция поиска по идентификаторам
        /// </summary>
        public static Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> FindOkFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultCollectionOk(entities => FindEntities(entities, id));

        /// <summary>
        /// Получить идентификаторы по добавляемым сущностям
        /// </summary>
        public static IResultCollection<TestEnum> AddRangeIdOk(IEnumerable<TestEntity> entities) =>
            entities.Select(entity => entity.Id).
            Map(ids => new ResultCollection<TestEnum>(ids));

        /// <summary>
        /// Функция получения по идентификатору. Не найдено
        /// </summary>
        public static Func<TestEnum, IResultValue<TestEntity>> FirstNotFoundFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultValueBindOk(entities => new ResultValue<TestEntity>(Errors.NotFoundError));

        /// <summary>
        /// Функция поиска по идентификаторам. Ошибка дублирования
        /// </summary>
        public static Func<IEnumerable<TestEnum>, IResultCollection<TestEntity>> FindDuplicateFunc(IResultCollection<TestEntity> entitiesResult) =>
            ids => entitiesResult.ResultCollectionBindOk(entities => new ResultCollection<TestEntity>(Errors.GetDuplicateError(ids)));
    }
}