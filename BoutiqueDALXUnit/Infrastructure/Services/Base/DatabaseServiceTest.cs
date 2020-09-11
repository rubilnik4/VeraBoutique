using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Services.Implementation;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base
{
    /// <summary>
    /// Базовый сервис получения данных из базы. Тесты
    /// </summary>
    public class DatabaseServiceTest
    {
        /// <summary>
        /// Проверить получение
        /// </summary>
        [Fact]
        public async Task Get_OK()
        {
            var testEntities = GetTestEntity();
            var testTableMock = GetTestDatabaseTable(testEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter.Object);

            var testResult = await testService.Get();
            var testEntitiesGet = testConverter.Object.FromEntities(testEntities.Value).ToList();

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.SequenceEqual(testEntitiesGet));
        }

        /// <summary>
        /// Проверить получение. Возврат с ошибкой базы данных
        /// </summary>
        [Fact]
        public async Task GetGenders_ErrorDatabase()
        {
            var errorInitial = TestDatabaseErrors.ErrorDatabase;
            var testDatabase = new ResultValue<ITestDatabase>(errorInitial);
            var testTable = testDatabase.ResultValueOk(database => database.TestTable);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabase, testTable, testConverter.Object);

            var testResult = await testService.Get();

            Assert.True(testResult.HasErrors);
            Assert.True(testResult.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Проверить получение по идентификатору
        /// </summary>
        [Fact]
        public async Task GetId_OK()
        {
            var testEntities = GetTestEntity();
            var testTableMock = GetTestDatabaseTable(testEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter.Object);

            var testResult = await testService.Get(It.IsAny<TestEnum>());
            var testEntitiesGet = testConverter.Object.FromEntity(FindEntity(testEntities.Value, testResult.Value.Id));

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.Equals(testEntitiesGet));
        }

        /// <summary>
        /// Проверить получение по идентификатору. ОШибка базы
        /// </summary>
        [Fact]
        public async Task GetId_DatabaseError()
        {
            var errorInitial = TestDatabaseErrors.ErrorDatabase;
            var testDatabase = new ResultValue<ITestDatabase>(errorInitial);
            var testTable = testDatabase.ResultValueOk(database => database.TestTable);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabase, testTable, testConverter.Object);

            var testResult = await testService.Get(It.IsAny<TestEnum>());

            Assert.True(testResult.HasErrors);
            Assert.True(testResult.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Проверить получение по идентификатору. ОШибка базы
        /// </summary>
        [Fact]
        public async Task GetId_NotFound()
        {
            var testEntities = GetTestEntity();
            var testTableMock = GetTestDatabaseTable(testEntities, FirstNotFoundFunc(testEntities));
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter.Object);

            var testResult = await testService.Get(It.IsAny<TestEnum>());

            Assert.True(testResult.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, testResult.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись типа пола
        /// </summary>
        [Fact]
        public async Task Post_OK()
        {
            var testDomains = EntityData.GetTestDomains();
            var testEntities = TestEntitiesEmpty;
            var testTableMock = GetTestDatabaseTable(testEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter.Object);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.OkStatus);
            Assert.True(resultIds.Value.SequenceEqual(EntityData.GetTestIds(testDomains)));
        }

        /// <summary>
        /// Проверить запись типа пола. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Post_DatabaseError()
        {
            var testDomains = EntityData.GetTestDomains();
            var errorInitial = TestDatabaseErrors.ErrorDatabase;
            var testDatabase = new ResultValue<ITestDatabase>(errorInitial);
            var testTable = testDatabase.ResultValueOk(database => database.TestTable);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabase, testTable, testConverter.Object);

            var testResult = await testService.Post(testDomains);

            Assert.True(testResult.HasErrors);
            Assert.True(testResult.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Проверить запись типа пола. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task Post_ErrorDuplicate()
        {
            var testDomains = EntityData.GetTestDomains();
            var testEntities = TestEntitiesEmpty;
            var testTableMock = GetTestDatabaseTable(testEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = GetTestEntityConverter();
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter.Object);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.OkStatus);
            Assert.True(resultIds.Value.SequenceEqual(EntityData.GetTestIds(testDomains)));
        }

        /// <summary>
        /// Получить тестовую таблицу в стандартном исполнении
        /// </summary>
        private static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities) =>
            GetTestDatabaseTable(testEntities, FirstOkFunc(testEntities));

        /// <summary>
        /// Получить тестовую таблицу
        /// </summary>
        private static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities,
                                                                     Func<TestEnum, IResultValue<TestEntity>> firstFunc) =>
            new Mock<ITestDatabaseTable>().
            Void(tableMock => tableMock.Setup(table => table.ToListAsync()).ReturnsAsync(testEntities)).
            Void(tableMock => tableMock.Setup(table => table.FirstAsync(It.IsAny<TestEnum>())).
                                        ReturnsAsync((TestEnum id) => testEntities.ToResultValue().
                                                                      ResultValueBindOk(entities => firstFunc(id)))).
            Void(tableMock => tableMock.Setup(table => table.AddRangeAsync(It.IsAny<IEnumerable<TestEntity>>())).
                                        ReturnsAsync((IEnumerable<TestEntity> entities) => AddRangeIdOk(entities)));

        /// <summary>
        /// Функция получения по идентификатору
        /// </summary>
        private static Func<TestEnum, IResultValue<TestEntity>> FirstOkFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultCollectionOkToValue(entities => FindEntity(entities, id));

        /// <summary>
        /// Получить идентификаторы по добавляемым сущностям
        /// </summary>
        private static IResultCollection<TestEnum> AddRangeIdOk(IEnumerable<TestEntity> entities) =>
            entities.Select(entity => entity.Id).
            Map(ids => new ResultCollection<TestEnum>(ids));

        /// <summary>
        /// Функция получения по идентификатору. Не найдено
        /// </summary>
        private static Func<TestEnum, IResultValue<TestEntity>> FirstNotFoundFunc(IResultCollection<TestEntity> entitiesResult) =>
            id => entitiesResult.ResultValueBindOk(entities => new ResultValue<TestEntity>(NotFoundError));

        /// <summary>
        /// Получить тестовую базу данных
        /// </summary>
        private static Mock<ITestDatabase> GetTestDatabase(ITestDatabaseTable testDatabaseTable) =>
            new Mock<ITestDatabase>().
            Void(databaseMock => databaseMock.Setup(database => database.TestTable).Returns(testDatabaseTable)).
            Void(databaseMock => databaseMock.Setup(database => database.SaveChangesAsync()).
                                              ReturnsAsync(new ResultError()));

        /// <summary>
        /// Получить конвертер сущностей
        /// </summary>
        private static Mock<ITestEntityConverter> GetTestEntityConverter() =>
            new Mock<ITestEntityConverter>().
            Void(converterMock => converterMock.Setup(converter => converter.FromEntities(It.IsAny<IEnumerable<TestEntity>>())).
                                                Returns(EntityData.GetTestDomains())).
            Void(converterMock => converterMock.Setup(converter => converter.ToEntities(It.IsAny<IEnumerable<ITestDomain>>())).
                                                Returns(EntityData.GetTestEntity())).
            Void(converterMock => converterMock.Setup(converter => converter.FromEntity(It.IsAny<TestEntity>())).
                                                Returns<TestEntity>(entity => FindDomain(EntityData.GetTestDomains(), entity.Id)));

        /// <summary>
        /// Получить базовый сервис получения данных из базы
        /// </summary>
        private static TestDatabaseService GetTestDatabaseService(ITestDatabase testDatabase, ITestDatabaseTable testDatabaseTable,
                                                                  ITestEntityConverter testConverter) =>
            GetTestDatabaseService(new ResultValue<ITestDatabase>(testDatabase),
                                   new ResultValue<ITestDatabaseTable>(testDatabaseTable),
                                   testConverter);

        /// <summary>
        /// Получить базовый сервис получения данных из базы
        /// </summary>
        private static TestDatabaseService GetTestDatabaseService(IResultValue<ITestDatabase> testDatabase,
                                                                  IResultValue<ITestDatabaseTable> testDatabaseTable,
                                                                  ITestEntityConverter testConverter) =>
            new TestDatabaseService(testDatabase, testDatabaseTable, testConverter);


        /// <summary>
        /// Получить тестовые сущности
        /// </summary>
        private static IResultCollection<TestEntity> GetTestEntity() =>
            new ResultCollection<TestEntity>(EntityData.GetTestEntity());

        /// <summary>
        /// Найти сущность
        /// </summary>
        private static TestEntity FindEntity(IEnumerable<TestEntity> entities, TestEnum id) =>
            entities.First(entity => entity.Id == id);

        /// <summary>
        /// Найти модель
        /// </summary>
        private static ITestDomain FindDomain(IEnumerable<ITestDomain> domains, TestEnum id) =>
            domains.First(domain => domain.Id == id);

        /// <summary>
        /// Пустая коллекция сущностей
        /// </summary>
        private static IResultCollection<TestEntity> TestEntitiesEmpty =>
           new ResultCollection<TestEntity>(Enumerable.Empty<TestEntity>());

        /// <summary>
        /// Ошибка ненайденного элемента
        /// </summary>
        private static IErrorResult NotFoundError => DatabaseErrors.ValueNotFoundError(String.Empty, String.Empty);
    }
}