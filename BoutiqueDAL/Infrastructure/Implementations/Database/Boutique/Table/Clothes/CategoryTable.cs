using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных категорий одежды
    /// </summary>
    public class CategoryTable : EntityDatabaseTable<string, CategoryEntity>, ICategoryTable
    {
        public CategoryTable(DbSet<CategoryEntity> categorySet)
            : base(categorySet)
        { }

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        protected override Expression<Func<CategoryEntity, bool>> IdPredicate(string id) =>
            entity => entity.Name == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        protected override Expression<Func<CategoryEntity, bool>> IdsPredicate(IEnumerable<string> ids) =>
            entity => ids.Contains(entity.Name);
    }
}