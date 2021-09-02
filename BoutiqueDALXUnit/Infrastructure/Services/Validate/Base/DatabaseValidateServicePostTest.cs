using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Validate.Base
{
    /// <summary>
    /// Базовый сервис проверки данных из базы. Запись. Тесты
    /// </summary>
    public class DatabaseValidateServicePostTest
    {
        /// <summary>
        /// Комплексная проверка сущности для записи
        /// </summary>
        [Fact]
        public async Task ValidatePost_Ok()
        {
            var test = TestData.TestDomains.First();
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(test);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Ошибка проверки модели
        /// </summary>
        [Fact]
        public async Task ValidatePost_ValidateModelError()
        {
            var test = TestData.TestDomains.First();
            var testInvalidName = new TestDomain(TestEnum.First, String.Empty, test.TestIncludes);
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(testInvalidName);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task ValidatePost_ValidateDuplicate()
        {
            var test = TestData.TestDomains.First();
            var testEntities = TestEntitiesData.TestEntities;
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(test);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueDuplicatedErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Ошибка проверки вложенных моделей
        /// </summary>
        [Fact]
        public async Task ValidatePost_ValidateIncludes()
        {
            var test = TestData.TestDomains.First();
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService(TestIncludeValidateServiceMock.ValidateFindsError());
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(test);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Комплексная проверка сущности для записи
        /// </summary>
        [Fact]
        public async Task ValidatePost_Collection_Ok()
        {
            var tests = TestData.TestDomains;
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(tests);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Ошибка проверки модели
        /// </summary>
        [Fact]
        public async Task ValidatePost_Collection_ValidateModelError()
        {
            var testInvalidName = new TestDomain(TestEnum.First, String.Empty, TestData.TestDomains.First().TestIncludes);
            var tests = TestData.TestDomains.Append(testInvalidName);
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService();
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(tests);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(result.Errors.First());
        }

        /// <summary>
        /// Комплексная проверка сущности для записи. Ошибка проверки вложенных моделей
        /// </summary>
        [Fact]
        public async Task ValidatePost_Collection_ValidateIncludes()
        {
            var tests = TestData.TestDomains;
            var testEntities = Enumerable.Empty<TestEntity>();
            var testIncludeValidateService = TestIncludeValidateServiceMock.GetTestIncludeValidateService(TestIncludeValidateServiceMock.ValidateFindsError());
            var dbSetTest = TestDatabaseSetMock.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidatePost(tests);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(result.Errors.First());
        }
    }
}