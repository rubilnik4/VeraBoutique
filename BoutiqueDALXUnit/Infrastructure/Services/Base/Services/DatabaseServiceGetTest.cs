using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks;
using BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.Tables;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Services
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
            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = new TestEntityConverter();
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var testResult = await testService.GetShort();
            var testEntitiesGet = testConverter.FromEntities(testResultEntities.Value);

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.SequenceEqual(testEntitiesGet.Value));
        }

        /// <summary>
        /// Проверить получение. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Get_Error()
        {
            var errorInitial = ErrorData.DatabaseError;
            var testResultEntities = new ResultCollection<TestEntity>(errorInitial);
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = new TestEntityConverter();
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var testResult = await testService.GetShort();

            Assert.True(testResult.HasErrors);
            Assert.Equal(errorInitial.ErrorResultType, testResult.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить получение по идентификатору
        /// </summary>
        [Fact]
        public async Task GetId_OK()
        {
            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTableGetMock.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = new TestEntityConverter();
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var testResult = await testService.GetShort(It.IsAny<TestEnum>());
            var testEntitiesGet = testConverter.FromEntity(SearchInEntities.FirstEntity(testResultEntities.Value, 
                                                                                        testResult.Value.Id));

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.Equals(testEntitiesGet.Value));
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
            var testConverter = new TestEntityConverter();
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, 
                                                                         testConverter);

            var testResult = await testService.GetShort(It.IsAny<TestEnum>());

            Assert.True(testResult.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, testResult.Errors.First().ErrorResultType);
        }

    }
}