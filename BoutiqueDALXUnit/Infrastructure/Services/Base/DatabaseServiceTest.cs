using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.CollectionExtensions;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Services.Implementation;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
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
            var genderConverter = GetTestEntityConverter();
            var genderService = GetTestDatabaseService(testDatabase, testTable, genderConverter.Object);

            var gendersResult = await genderService.Get();

            Assert.True(gendersResult.HasErrors);
            Assert.True(gendersResult.Errors.First().Equals(errorInitial));
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
        /// Получить тестовую таблицу
        /// </summary>
        private static Mock<ITestDatabaseTable> GetTestDatabaseTable(IResultCollection<TestEntity> testEntities) =>
            new Mock<ITestDatabaseTable>().
            Void(tableMock => tableMock.Setup(table => table.ToListAsync()).ReturnsAsync(testEntities)).
            Void(tableMock => tableMock.Setup(table => table.FirstAsync(It.IsAny<TestEnum>())).
                                        ReturnsAsync((TestEnum id) => testEntities.ToResultValue().
                                                                      ResultValueOk(entities => FindEntity(entities, id))));

        /// <summary>
        /// Получить тестовую базу данных
        /// </summary>
        private static Mock<ITestDatabase> GetTestDatabase(ITestDatabaseTable testDatabaseTable) =>
            new Mock<ITestDatabase>().
            Void(databaseMock => databaseMock.Setup(database => database.TestTable).Returns(testDatabaseTable));

        /// <summary>
        /// Получить конвертер сущностей
        /// </summary>
        private static Mock<ITestEntityConverter> GetTestEntityConverter() =>
            new Mock<ITestEntityConverter>().
            Void(converterMock => converterMock.Setup(converter => converter.FromEntities(It.IsAny<IEnumerable<TestEntity>>())).
                                                Returns(EntityData.GetTestDomains())).
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
    }
}