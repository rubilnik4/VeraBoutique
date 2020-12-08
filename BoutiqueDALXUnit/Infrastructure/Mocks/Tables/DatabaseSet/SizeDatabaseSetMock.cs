using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных размеров одежды
    /// </summary>
    public class SizeDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<SizeEntity>> GetSizeDbSet(IEnumerable<SizeEntity> sizes) =>
            sizes.AsQueryable().BuildMockDbSet();
    }
}