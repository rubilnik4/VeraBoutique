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
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<TestEntity, bool>> IdPredicate(TestEnum id) =>
            entity => entity.TestEnum == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<TestEntity, bool>> IdsPredicate(IEnumerable<TestEnum> ids) =>
            entity => ids.Contains(entity.TestEnum);
    }
}