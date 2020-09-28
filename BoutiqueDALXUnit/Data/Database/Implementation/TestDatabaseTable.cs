using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
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
        public TestDatabaseTable(DbSet<TestEntity> testSet)
            :base(testSet)
        {
            _testSet = testSet;
        }

        /// <summary>
        /// Таблица типа пола
        /// </summary>
        private readonly DbSet<TestEntity> _testSet;

        /// <summary>
        /// Поиск первого с включением сущностей
        /// </summary>
        protected override async Task<TestEntity?> FirstAsync<TIdOut, TEntityOut>(TestEnum id,
                                                                                  Expression<Func<TestEntity, IEnumerable<TEntityOut>>> include) =>
            await _testSet.
                Include(include).
                FirstOrDefaultAsync(testEntity => testEntity.TestEnum == id);

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        protected override IQueryable<TestEntity> Where(IEnumerable<TestEnum> ids) =>
            _testSet.Where(genderEntity => ids.Contains(genderEntity.TestEnum));

        /// <summary>
        /// Поиск по параметрам с включением сущностей
        /// </summary>
        protected override IQueryable<TestEntity> Where<TIdOut, TEntityOut>(IEnumerable<TestEnum> ids,
                                                                            Expression<Func<TestEntity, IEnumerable<TEntityOut>>> include) =>
            _testSet.
            Include(include).
            Where(testEntity => ids.Contains(testEntity.TestEnum));
    }
}