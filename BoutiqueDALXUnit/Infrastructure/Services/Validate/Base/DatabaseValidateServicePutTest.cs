using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.Models.Enums;
using Functional.Models.Interfaces.Errors.DatabaseErrors;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Base
{
    /// <summary>
    /// Базовый сервис проверки данных из базы. Обновление. Тесты
    /// </summary>
    public class DatabaseValidateServicePutTest
    {
        /// <summary>
        /// Комплексная проверка сущности для обновления
        /// </summary>
        [Fact]
        public async Task ValidatePut_Ok()
        {
            var test = TestData.TestDomains.First();
            var testEntities = TestEntitiesData.TestEntities;
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePut(test);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Комплексная проверка сущности для обновления. Ошибка проверки модели
        /// </summary>
        [Fact]
        public async Task ValidatePut_ValidateModelError()
        {
            var test = TestData.TestDomains.First();
            var testInvalidName = new TestDomain(TestEnum.First, String.Empty, test.TestIncludes);
            var testEntities = TestEntitiesData.TestEntities;
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePut(testInvalidName);

            Assert.True(result.HasErrors);
            Assert.IsType<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Сущность не найдена
        /// </summary>
        [Fact]
        public async Task ValidatePut_ValidateNotFound()
        {
            var test = TestData.TestDomains.First();
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePut(test);

            Assert.True(result.HasErrors);
            Assert.IsType<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Ошибка проверки вложенных моделей
        /// </summary>
        [Fact]
        public async Task ValidatePut_ValidateIncludes()
        {
            var test = TestData.TestDomains.First();
            var testEntities = TestEntitiesData.TestEntities;
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService(TestIncludeValidateServiceMock.ValidateFindsError());
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePut(test);

            Assert.True(result.HasErrors);
            Assert.IsType<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }
    }
}