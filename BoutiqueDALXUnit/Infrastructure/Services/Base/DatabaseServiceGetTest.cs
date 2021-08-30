using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Database.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors.DatabaseErrors;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors.DatabaseErrors;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base
{
    /// <summary>
    /// Базовый сервис получения данных из базы. Тесты
    /// </summary>
    public class DatabaseServiceGetTest
    {
        /// <summary>
        /// Проверить получение
        /// </summary>
        [Fact]
        public async Task Get_OK()
        {
            var testEntities = TestEntitiesData.TestEntities;
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock, testConverter);

            var testResult = await testService.Get();
            var testEntitiesGet = testConverter.FromEntities(testEntities);

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.SequenceEqual(testEntitiesGet.Value));
        }

        /// <summary>
        /// Проверить получение. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Get_Error()
        {
            var errorInitial = DatabaseErrorData.TableError;
            var testResultEntities = new ResultCollection<TestEntity>(errorInitial);
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var testResult = await testService.Get();

            Assert.True(testResult.HasErrors);
            Assert.IsNotType<DatabaseTableErrorResult>(testResult.Errors.First());
        }

        /// <summary>
        /// Проверить получение по идентификатору
        /// </summary>
        [Fact]
        public async Task GetId_OK()
        {
            var testEntities = TestEntitiesData.TestEntities;
            var testEntity = testEntities.First();
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock,
                                                                         testConverter);

            var testResult = await testService.Get(testEntity.Id);
            var testEntityGet = testConverter.FromEntity(testEntity);

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.Equals(testEntityGet.Value));
        }

        /// <summary>
        /// Проверить получение по идентификатору. Ошибка
        /// </summary>
        [Fact]
        public async Task GetId_Error()
        {
            var errorInitial = DatabaseErrorData.TableError;
            var testResultEntities = new ResultCollection<TestEntity>(errorInitial);
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var testResult = await testService.Get(It.IsAny<TestEnum>());
         
            Assert.True(testResult.HasErrors);
            Assert.IsNotType<DatabaseTableErrorResult>(testResult.Errors.First());
        }

        /// <summary>
        /// Проверить получение по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetId_NotFound()
        {
            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testResultEntities,
                                                                          DatabaseTableGetMock.FirstNotFoundFunc(testResultEntities));
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, 
                                                                         testConverter);

            var testResult = await testService.Get(It.IsAny<TestEnum>());

            Assert.True(testResult.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(testResult.Errors.First());
        }

    }
}