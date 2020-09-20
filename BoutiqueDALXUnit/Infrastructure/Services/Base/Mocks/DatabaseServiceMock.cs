using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Services.Implementation;
using BoutiqueDALXUnit.Data.Services.Interfaces;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

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
        public static TestDatabaseService GetTestDatabaseService(ITestDatabase testDatabase, ITestDatabaseTable testDatabaseTable,
                                                                 ITestEntityConverter testConverter) =>
            new TestDatabaseService(testDatabase, testDatabaseTable, testConverter);
    }
}