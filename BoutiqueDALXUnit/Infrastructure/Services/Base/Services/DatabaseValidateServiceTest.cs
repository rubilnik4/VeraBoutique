using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.Models.Enums;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Services
{
    /// <summary>
    /// Базовый сервис проверки данных из базы. Тесты
    /// </summary>
    public class DatabaseValidateServiceTest
    {
        /// <summary>
        /// Получить ошибку дублирования. Корректный вариант
        /// </summary>
        [Fact]
        public async Task ValidateDuplicate_Ok()
        {
            var testDomain = TestData.TestDomains.Last();
            var tests = Enumerable.Empty<TestEntity>();
            var databaseValidateService = GetDatabaseValidateService(tests);

            var result = await databaseValidateService.ValidateDuplicate(testDomain.Id);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Получить ошибку дублирования. Ошибки дублирования
        /// </summary>
        [Fact]
        public async Task ValidateDuplicate_ErrorDuplicate()
        {
            var testDomain = TestData.TestDomains.First();
            var tests = TestEntitiesData.TestEntities;
            var databaseValidateService = GetDatabaseValidateService(tests);

            var result = await databaseValidateService.ValidateDuplicate(testDomain.Id);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueDuplicate);
        }

        /// <summary>
        /// Получить ошибки дублирования. Корректный вариант
        /// </summary>
        [Fact]
        public async Task ValidateDuplicates_Ok()
        {
            var testDomains = TestData.TestDomains;
            var tests = Enumerable.Empty<TestEntity>();
            var databaseValidateService = GetDatabaseValidateService(tests);

            var result = await databaseValidateService.ValidateDuplicates(testDomains.Select(domain => domain.Id));

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Получить ошибки дублирования. Ошибки дублирования
        /// </summary>
        [Fact]
        public async Task ValidateDuplicates_ErrorDuplicate()
        {
            var testDomains = TestData.TestDomains;
            var tests = TestEntitiesData.TestEntities;
            var databaseValidateService = GetDatabaseValidateService(tests);

            var result = await databaseValidateService.ValidateDuplicates(testDomains.Select(domain => domain.Id));

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.DatabaseValueDuplicate);
        }

        /// <summary>
        /// Сущность базы данных
        /// </summary>
        private static Mock<DbSet<TestEntity>> GetDbSet(IEnumerable<TestEntity> testEntities) =>
            testEntities.AsQueryable().BuildMockDbSet();

        /// <summary>
        /// Тестовая таблица базы данных
        /// </summary>
        private static ITestTable GetTestTable(IEnumerable<TestEntity> testEntities) =>
            new TestTable(GetDbSet(testEntities).Object);

        /// <summary>
        /// Тестовый сервис проверки вложенных данных
        /// </summary>
        private static Mock<ITestIncludeDatabaseValidateService> TestIncludeDatabaseValidateService =>
            new Mock<ITestIncludeDatabaseValidateService>();

        /// <summary>
        /// Тестовый сервис проверки данных из базы
        /// </summary>
        private static ITestDatabaseValidateService GetDatabaseValidateService(IEnumerable<TestEntity> testEntities) =>
             new TestDatabaseValidateService(GetTestTable(testEntities), TestIncludeDatabaseValidateService.Object);
    }
}