using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using ResultFunctional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Тестовые данные таблицы категории одежды
    /// </summary>
    public static class CategoryTableMock
    {
        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public static ICategoryTable GetCategoryTable(IEnumerable<CategoryEntity> categories) =>
            new CategoryTable(CategoryDatabaseSetMock.GetCategoryDbSet(categories).Object);
    }
}