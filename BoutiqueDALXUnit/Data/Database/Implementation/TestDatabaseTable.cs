using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
        {
            _testSet = testSet;
        }

        /// <summary>
        /// Таблица типа пола
        /// </summary>
        private readonly DbSet<TestEntity> _testSet;

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected override IQueryable<TestEntity> Where(IEnumerable<TestEnum> ids) =>
            _testSet.Where(genderEntity => ids.Contains(genderEntity.TestEnum));
    }
}