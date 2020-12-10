using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных цвета одежды
    /// </summary>
    public static class ColorClothesDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<ColorEntity>> GetColorClothesDbSet(IEnumerable<ColorEntity> colors) =>
            colors.AsQueryable().BuildMockDbSet();
    }
}