using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Database.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base
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
            var testTableMock = DatabaseTablePutMock.GetTestDatabaseTable(TestEntitiesData.TestEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock);
            var validateService = TestValidateServicePutMock.GetDatabaseValidateService();
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock,
                                                                         validateService.Object, testConverter);

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
            var testTableMock = DatabaseTablePutMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = TestValidateServicePutMock.GetDatabaseValidateService(TestValidateServicePutMock.ValueNotFoundFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var result = await testService.Put(testDomainPut);

            Assert.True(result.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, result.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить обновление. Ошибка обновления
        /// </summary>
        [Fact]
        public async Task Put_UpdateError()
        {
            var testDomainPut = TestData.TestDomains.First();
            var testTableMock = DatabaseTablePutMock.GetTestDatabaseTable(DatabaseTablePutMock.UpdateError());
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = TestValidateServicePutMock.GetDatabaseValidateService();
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var result = await testService.Put(testDomainPut);

            Assert.True(result.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, result.Errors.First().ErrorResultType);
        }
    }
}