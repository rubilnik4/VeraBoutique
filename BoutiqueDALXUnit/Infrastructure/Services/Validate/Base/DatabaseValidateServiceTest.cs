﻿using System.Linq;
using System.Threading.Tasks;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate.TestValidate;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.Models.Enums;
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
            var dbSetTest = TestDatabaseSet.GetDbSetTest(testEntities);
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
            var dbSetTest = TestDatabaseSet.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidateFind(id);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
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
            var dbSetTest = TestDatabaseSet.GetDbSetTest(testEntities);
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
            var dbSetTest = TestDatabaseSet.GetDbSetTest(testEntities);
            var testTable = new TestTable(dbSetTest.Object);
            var testValidateService = new TestDatabaseValidateService(testTable, testIncludeValidateService.Object);

            var result = await testValidateService.ValidateFinds(ids);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}