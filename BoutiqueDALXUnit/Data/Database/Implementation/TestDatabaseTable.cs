using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Тестовая таблица базы данных
    /// </summary>
    public class TestDatabaseTable: EntityDatabaseTable<TestEnum, TestEntity>, ITestDatabaseTable
    {
        public TestDatabaseTable(DbSet<TestEntity> testSet, string tableName)
            :base(testSet, tableName)
        { }
    }
}