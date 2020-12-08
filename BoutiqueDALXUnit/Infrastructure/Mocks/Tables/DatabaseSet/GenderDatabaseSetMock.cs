using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных типа пола
    /// </summary>
    public static class GenderDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<GenderEntity>> GetGenderDbSet(IEnumerable<GenderEntity> genders) =>
            genders.AsQueryable().BuildMockDbSet();
    }
}