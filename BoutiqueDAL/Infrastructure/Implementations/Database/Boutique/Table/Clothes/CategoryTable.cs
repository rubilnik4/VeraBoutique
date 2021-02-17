using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base.EntityDatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных категорий одежды
    /// </summary>
    public class CategoryTable : EntityDatabaseTable<string, ICategoryDomain, CategoryEntity>, ICategoryTable
    {
        public CategoryTable(DbSet<CategoryEntity> categorySet)
            : base(categorySet)
        { }

        /// <summary>
        /// Выгрузка идентификатора
        /// </summary>
        public override Expression<Func<CategoryEntity, string>> IdSelect() =>
            clothesType => clothesType.Name;

        /// <summary>
        /// Функция поиска по идентификатору
        /// </summary>
        public override Expression<Func<CategoryEntity, bool>> IdPredicate(string id) =>
            entity => entity.Name == id;

        /// <summary>
        /// Функция поиска по параметрам
        /// </summary>
        public override Expression<Func<CategoryEntity, bool>> IdsPredicate(IEnumerable<string> ids) =>
            entity => ids.Contains(entity.Name);
    }
}