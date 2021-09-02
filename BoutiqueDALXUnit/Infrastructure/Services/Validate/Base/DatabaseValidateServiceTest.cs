using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Base
{
    /// <summary>
    /// Базовый сервис проверки данных из базы. Тесты
    /// </summary>
    public class DatabaseValidateServiceTest
    {
        /// <summary>
        /// Проверка наличия элемента
        /// </summary>
        [Fact]
        public async Task ValidateFind_Ok()
        {
            var testEntities = TestEntitiesData.TestEntities;
            var id = testEntities.First().Id;
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result =  await testValidateService.ValidateFind(id);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверка наличия элемента. Элемент не найден
        /// </summary>
        [Fact]
        public async Task ValidateFind_NotFound()
        {
            var testEntities = Enumerable.Empty<TestEntity>();
            var id = TestEntitiesData.TestEntities.First().Id;
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidateFind(id);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверка наличия элементов
        /// </summary>
        [Fact]
        public async Task ValidateFinds_Ok()
        {
            var testEntities = TestEntitiesData.TestEntities;
            var ids = testEntities.Select(testEntity => testEntity.Id);
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidateFinds(ids);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверка наличия элементов. Элементы не найдены
        /// </summary>
        [Fact]
        public async Task ValidateFinds_NotFound()
        {
            var testEntities = Enumerable.Empty<TestEntity>().ToList();
            var ids = TestEntitiesData.TestEntities.Select(testEntity => testEntity.Id);
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidateFinds(ids);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Проверка количества элементов
        /// </summary>
        [Fact]
        public void ValidateQuantity_Ok()
        {
            var tests = TestData.TestDomains;
            var testEntities = Enumerable.Empty<TestEntity>().ToList();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = testValidateService.ValidateQuantity(tests);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверка количества элементов. Ошибка
        /// </summary>
        [Fact]
        public void ValidateQuantity_ErrorCollectionEmpty()
        {
            var tests = Enumerable.Empty<ITestDomain>().ToList();
            var testEntities = Enumerable.Empty<TestEntity>().ToList();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = testValidateService.ValidateQuantity(tests);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }
    }
}