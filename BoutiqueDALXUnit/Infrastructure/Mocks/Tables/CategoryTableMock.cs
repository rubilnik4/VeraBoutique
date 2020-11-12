using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks
{
    /// <summary>
    /// Тестовые данные таблицы категории одежды
    /// </summary>
    public static class CategoryTableMock
    {
        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public static Mock<ICategoryTable> GetCategoryTable(Func<string, IQueryable<CategoryEntity>> categoryFunc) =>
            new Mock<ICategoryTable>().
            Void(mock => mock.Setup(categoryTable => categoryTable.Where(It.IsAny<string>())).
                              Returns(categoryFunc));

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        public static Func<string, IQueryable<CategoryEntity>> GetCategoryOk(IEnumerable<CategoryEntity> categoryEntities) =>
            category => categoryEntities.Where(categoryEntity => categoryEntity.Id == category).
                                         AsQueryable().BuildMock().Object;
    }
}