﻿using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Database.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTable;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Services
{
    public class DatabaseServiceDeleteTest
    {
        /// <summary>
        /// Проверить удаление
        /// </summary>
        [Fact]
        public async Task Delete_Ok()
        {
            var testDelete = TestData.TestDomains.Last();
            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTableDeleteTest.GetTestDatabaseTable(testResultEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

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
            var testDelete = TestData.TestDomains.Last();
            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTableDeleteTest.GetTestDatabaseTable(testResultEntities,
                                                                             DatabaseTableGetMock.FirstNotFoundFunc(testResultEntities));
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, resultEntity.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить удаление. Ошибка удаления
        /// </summary>
        [Fact]
        public async Task Delete_DeleteError()
        {
            var testDelete = TestData.TestDomains.Last();
            var testResultEntities = TestEntitiesData.TestResultEntities;
            var testTableMock = DatabaseTableDeleteTest.GetTestDatabaseTable(testResultEntities, DatabaseTableDeleteTest.DeleteErrorFunc());
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, resultEntity.Errors.First().ErrorResultType);
        }
    }
}