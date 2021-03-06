﻿using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
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
            var validateService = TestValidateServicePostMock.GetDatabaseValidateService();
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
        public async Task Post_Value_ValidateError()
        {
            var testDomain = TestData.TestDomains.First();
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = TestValidateServicePostMock.GetDatabaseValidateService(TestValidateServicePostMock.DuplicateValueFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultId = await testService.Post(testDomain);

            Assert.True(resultId.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueDuplicate, resultId.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task Post_Value_SavingError()
        {
            var testDomain = TestData.TestDomains.First();
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable(DatabaseTablePostMock.AddIdError());
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = TestValidateServicePostMock.GetDatabaseValidateService();
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultId = await testService.Post(testDomain);

            Assert.True(resultId.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, resultId.Errors.First().ErrorResultType);
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
            var validateService = TestValidateServicePostMock.GetDatabaseValidateService();
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
        public async Task Post_Collection_Error()
        {
            var testDomains = TestData.TestDomains;
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable();
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = TestValidateServicePostMock.GetDatabaseValidateService(TestValidateServicePostMock.DuplicateCollectionFunc());
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseValueDuplicate, resultIds.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Проверить запись. Ошибка дублирования
        /// </summary>
        [Fact]
        public async Task Post_Collection_SavingError()
        {
            var testDomains = TestData.TestDomains;
            var testTableMock = DatabaseTablePostMock.GetTestDatabaseTable(DatabaseTablePostMock.AddRangeIdError());
            var testDatabaseMock = DatabaseMock.GetTestDatabase(testTableMock.Object);
            var validateService = TestValidateServicePostMock.GetDatabaseValidateService();
            var testConverter = TestEntityConverterMock.TestEntityConverter;
            var testService = DatabaseServiceMock.GetTestDatabaseService(testDatabaseMock.Object, testTableMock.Object,
                                                                         validateService.Object, testConverter);

            var resultIds = await testService.Post(testDomains);

            Assert.True(resultIds.HasErrors);
            Assert.Equal(ErrorResultType.DatabaseTableAccess, resultIds.Errors.First().ErrorResultType);
        }
    }
}