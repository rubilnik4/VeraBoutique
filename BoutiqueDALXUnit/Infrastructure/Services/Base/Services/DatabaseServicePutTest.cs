using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks;
using BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.Tables;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Services
{
    /// <summary>
    /// Базовый сервис обновления данных из базы. Тесты
    /// </summary>
    public class DatabaseServicePutTest
    {

        /// <summary>
        /// Проверить обновление
        /// </summary>
        [Fact]
        public async Task Put_Ok()
        {
            var testDomainPut = TestData.TestDomains.First();
            testDomainPut.Name = "ChangeName";

            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTablePutMock.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = new TestEntityConverter();
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var result = await testService.Put(testDomainPut);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить обновление. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Put_NotFound()
        {
            var testDomainPut = TestData.TestDomains.First();
            testDomainPut.Name = "ChangeName";

            var testResultEntities = TestEntitiesData.TestResultEntitiesEmpty;
            var testTableMock = DatabaseTablePutMock.GetTestDatabaseTable(testResultEntities,
                                                                          DatabaseTableGetMock.FirstNotFoundFunc(testResultEntities));
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = new TestEntityConverter();
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var result = await testService.Put(testDomainPut);

            Assert.True(result.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, result.Errors.First().ErrorResultType);
        }
    }
}