using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Entities;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks;
using BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks.Tables;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Services
{
    /// <summary>
    /// Базовый сервис отправки данных из базы. Тесты
    /// </summary>
    public class DatabaseServicePostTest
    {
        /// <summary>
        /// Проверить запись
        /// </summary>
        [Fact]
        public async Task Post_Value_OK()
        {
            var testDomain = TestData.TestDomains.First();
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = DatabaseValidateServiceMock.GetDatabaseValidateService();
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object, 
                                                                         validateService.Object, testConverter);

            var resultId = await testService.Post(testDomain);

            Assert.True(resultId.OkStatus);
            Assert.True(resultId.Value.Equals(testDomain.Id));
        }

        /// <summary>
        /// Проверить запись. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task Post_Value_ErrorDuplicate()
        {
            var testDomain = TestData.TestDomains.First();
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = DatabaseValidateServiceMock.GetDatabaseValidateServiceDuplicate(DatabaseValidateServiceMock.DuplicateFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultId = await testService.Post(testDomain);

            Assert.True(resultId.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueDuplicate, resultId.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись. Элемент не прошел проверку
        /// </summary>
        [Fact]
        public async Task Post_NonValid()
        {
            var testDomainPut = TestData.TestDomains.First();
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = DatabaseValidateServiceMock.GetDatabaseValidateServiceValidateValue(DatabaseValidateServiceMock.NonValidFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var result = await testService.Post(testDomainPut);

            Assert.True(result.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, result.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись
        /// </summary>
        [Fact]
        public async Task Post_Collection_OK()
        {
            var testDomains = TestData.TestDomains;
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = DatabaseValidateServiceMock.GetDatabaseValidateService();
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.OkStatus);
            Assert.True(resultIds.Value.SequenceEqual(TestData.GetTestIds(testDomains)));
        }

        /// <summary>
        /// Проверить запись. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task Post_Collection_ErrorDuplicate()
        {
            var testDomains = TestData.TestDomains;
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = DatabaseValidateServiceMock.GetDatabaseValidateServiceDuplicates(DatabaseValidateServiceMock.DuplicatesFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueDuplicate, resultIds.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись. Элементы не прошли проверку
        /// </summary>
        [Fact]
        public async Task Post_Collection_NonValid()
        {
            var testDomains = TestData.TestDomains;
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = DatabaseValidateServiceMock.GetDatabaseValidateServiceValidateCollection(DatabaseValidateServiceMock.NonValidCollectionFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.HasErrors);
            Assert.Equal(ErrorResultType.ValueNotFound, resultIds.Errors.First().ErrorResultType);
        }
    }
}