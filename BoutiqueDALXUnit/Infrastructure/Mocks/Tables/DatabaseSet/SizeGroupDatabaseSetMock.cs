using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных группы размеров
    /// </summary>
    public class SizeGroupDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<SizeGroupEntity>> GetSizeGroupDbSet(IEnumerable<SizeGroupEntity> sizeGroups) =>
            sizeGroups.AsQueryable().BuildMockDbSet();
    }
}