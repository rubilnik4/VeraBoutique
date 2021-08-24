using BoutiqueDALXUnit.Data.Database.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Results;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Database.Base
{
    /// <summary>
    /// Тестовую база данных
    /// </summary>
    public static class DatabaseMock
    {
        /// <summary>
        /// Получить тестовую базу данных
        /// </summary>
        public static Mock<ITestDatabase> GetTestDatabase(ITestTable testTable) =>
            new Mock<ITestDatabase>().
            Void(databaseMock => databaseMock.Setup(database => database.TestTable).Returns(testTable)).
            Void(databaseMock => databaseMock.Setup(database => database.SaveChangesAsync()).
                                              ReturnsAsync(new ResultError()));
    }
}