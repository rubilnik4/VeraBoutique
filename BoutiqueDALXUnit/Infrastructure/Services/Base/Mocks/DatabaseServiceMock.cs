using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.Base.Mocks
{
    /// <summary>
    /// Базовый сервис получения данных из базы
    /// </summary>
    public static class DatabaseServiceMock
    {
        /// <summary>
        /// Получить базовый сервис получения данных из базы
        /// </summary>
        public static ITestDatabaseService GetTestDatabaseService(ITestDatabase testDatabase, ITestTable testTable,
                                                                  ITestEntityConverter testConverter) =>
            new TestDatabaseService(testDatabase, testTable, GetTestDatabaseValidateService(testTable), testConverter);

        /// <summary>
        /// Получить базовый сервис получения данных из базы
        /// </summary>
        public static ITestDatabaseService GetTestDatabaseService(ITestDatabase testDatabase, ITestTable testTable,
                                                                  ITestDatabaseValidateService testDatabaseValidateService,
                                                                  ITestEntityConverter testConverter) =>
            new TestDatabaseService(testDatabase, testTable, testDatabaseValidateService, testConverter);

        /// <summary>
        /// Получить тестовый сервис проверки данных
        /// </summary>
        public static ITestDatabaseValidateService GetTestDatabaseValidateService(ITestTable testTable) =>
            new TestDatabaseValidateService(testTable, new Mock<ITestIncludeDatabaseValidateService>().Object);
    }
}