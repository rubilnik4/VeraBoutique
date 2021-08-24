using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Mocks.Database.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Services.Base;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.TestTables;
using Functional.Models.Enums;
using Functional.Models.Implementations.Results;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base
{
    public class DatabaseServiceDeleteTest
    {

        /// <summary>
        /// Удаление по идентификатору
        /// </summary>
        [Fact]
        public async Task DeleteAll_Ok()
        {
            var testEntities = TestEntitiesData.TestEntities;
            var testTableMock = DatabaseTableDeleteMock.GetTestDatabaseTable(testEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock, testConverter);

            var resultEntity = await testService.Delete();

            Assert.True(resultEntity.OkStatus);
        }

        /// <summary>
        /// Удаление по идентификатору. Ошибка удаления
        /// </summary>
        [Fact]
        public async Task DeleteAll_DeleteError()
        {
            var errorInitial = ErrorData.DatabaseErrorType;
            var resultDelete = new ResultError(errorInitial);
            var testTableMock = DatabaseTableDeleteMock.GetTestDatabaseTable(resultDelete);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var resultEntity = await testService.Delete();

            Assert.True(resultEntity.HasErrors);
            Assert.True(errorInitial.Equals(resultEntity.Errors.First()));
        }

        /// <summary>
        /// Удаление по идентификатору
        /// </summary>
        [Fact]
        public async Task Delete_Ok()
        {
            var testDelete = TestData.TestDomains.Last();
            var testEntities = TestEntitiesData.TestEntities;
            var testTableMock = DatabaseTableDeleteMock.GetTestDatabaseTable(testEntities);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock, testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.OkStatus);
            Assert.True(testDelete.Equals(resultEntity.Value));
        }

        /// <summary>
        /// Удаление по идентификатору. Ошибка удаления
        /// </summary>
        [Fact]
        public async Task Delete_DeleteError()
        {
            var testDelete = TestData.TestDomains.Last();
            var testTableMock = DatabaseTableDeleteMock.GetTestDatabaseTable(DatabaseTableDeleteMock.DeleteError);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, resultEntity.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Удаление по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Delete_NotFound()
        {
            var testDelete = TestData.TestDomains.Last();
            var testTableMock = DatabaseTableDeleteMock.GetTestDatabaseTable(DatabaseTableDeleteMock.FirstNotFound);
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         testConverter);

            var resultEntity = await testService.Delete(testDelete.Id);

            Assert.True(resultEntity.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, resultEntity.Errors.First().ErrorResultType);
        }
    }
}