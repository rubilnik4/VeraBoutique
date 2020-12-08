using System.Collections.Generic;
using System.Linq;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Тестовые сущности базы данных
    /// </summary>
    public static class TestDatabaseSetMock
    {
        /// <summary>
        /// Получить тестовые сущности базы данных
        /// </summary>
        public static Mock<DbSet<TestEntity>> GetDbSetTest(IEnumerable<TestEntity> testEntities) =>
           testEntities.AsQueryable().BuildMockDbSet();
    }
}