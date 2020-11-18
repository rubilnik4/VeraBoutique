using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Тестовая таблица базы данных
    /// </summary>
    public class TestTable : EntityDatabaseTable<TestEnum, ITestDomain, TestEntity>, ITestTable
    {
        public TestTable(DbSet<TestEntity> testSet)
          : base(testSet)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<TestEntity, TestEnum>> IdSelect() =>
            entity => entity.TestEnum;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<TestEntity, bool>> IdPredicate(TestEnum id) =>
            entity => entity.TestEnum == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<TestEntity, bool>> IdsPredicate(IEnumerable<TestEnum> ids) =>
            entity => ids.Contains(entity.TestEnum);
    }
}