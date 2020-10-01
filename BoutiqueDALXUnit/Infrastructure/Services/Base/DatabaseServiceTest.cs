using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDALXUnit.Data;
using BoutiqueDALXUnit.Data.Database.Implementation;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Moq;
using Xunit;
using static BoutiqueDALXUnit.Data.Services.Implementation.SearchInModels;
using static BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.DatabaseTableMock;
using static BoutiqueDALXUnit.Data.EntityData;
using static BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.DatabaseServiceMock;
using static BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.DatabaseMock;

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
            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = new TestEntityConverter();
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var testResult = await testService.Get();
            var testEntitiesGet = testConverter.FromEntities(testResultEntities.Value).ToList();

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.SequenceEqual(testEntitiesGet));
        }

        /// <summary>
        /// Проверить получение по идентификатору
        /// </summary>
        [Fact]
        public async Task GetId_OK()
        {
            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var testResult = await testService.Get(It.IsAny<TestEnum>());
            var testEntitiesGet = testConverter.FromEntity(FirstEntity(testResultEntities.Value, testResult.Value.Id));

            Assert.True(testResult.OkStatus);
            Assert.True(testResult.Value.Equals(testEntitiesGet));
        }

        /// <summary>
        /// Проверить получение по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetId_NotFound()
        {
            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities, FirstNotFoundFunc(testResultEntities));
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var testResult = await testService.Get(It.IsAny<TestEnum>());

            Assert.True(testResult.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, testResult.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись
        /// </summary>
        [Fact]
        public async Task Post_OK()
        {
            var testDomains = TestData.GetTestDomains();
            var testResultEntities = TestResultEntitiesEmpty;
            var testTableMock = GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.OkStatus);
            Assert.True(resultIds.Value.SequenceEqual(TestData.GetTestIds(testDomains)));
        }

        /// <summary>
        /// Проверить запись. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task Post_ErrorDuplicate()
        {
            var testDomains = TestData.GetTestDomains();
            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities, FindDuplicateFunc(testResultEntities));
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueDuplicate, resultIds.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить обновление
        /// </summary>
        [Fact]
        public async Task Put_Ok()
        {
            var testDomainPut = TestData.GetTestDomains().First();
            testDomainPut.Name = "ChangeName";

            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var result = await testService.Put(testDomainPut);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Проверить обновление. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Put_NotFound()
        {
            var testDomainPut = TestData.GetTestDomains().First();
            testDomainPut.Name = "ChangeName";

            var testResultEntities = TestResultEntitiesEmpty;
            var testTableMock = GetTestDatabaseTable(testResultEntities, FirstNotFoundFunc(testResultEntities));
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var result = await testService.Put(testDomainPut);

            Assert.True(result.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, result.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить удаление
        /// </summary>
        [Fact]
        public async Task Delete_Ok()
        {
            var testDelete = TestData.GetTestDomains().Last();
            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.OkStatus);
            Assert.True(testDelete.Equals(resultEntity.Value));
        }

        /// <summary>
        /// Проверить удаление. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Delete_NotFound()
        {
            var testDelete = TestData.GetTestDomains().Last();
            var testResultEntities = TestResultEntities;
            var testTableMock = GetTestDatabaseTable(testResultEntities, FirstNotFoundFunc(testResultEntities));
            var testDatabaseMock = GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverter;
            var testService = GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueNotFound, resultEntity.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Конвертер в сущность базы данных
        /// </summary>
        private static ITestEntityConverter TestEntityConverter => new TestEntityConverter();
    }
}