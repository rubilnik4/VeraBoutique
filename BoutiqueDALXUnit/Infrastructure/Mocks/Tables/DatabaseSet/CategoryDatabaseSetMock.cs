using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных категорий одежды
    /// </summary>
    public static class CategoryDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<CategoryEntity>> GetCategoryDbSet(IEnumerable<CategoryEntity> categories) =>
            categories.AsQueryable().BuildMockDbSet();
    }
}