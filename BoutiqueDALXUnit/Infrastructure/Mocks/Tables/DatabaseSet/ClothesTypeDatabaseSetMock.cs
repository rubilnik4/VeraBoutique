using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных Типов одежды
    /// </summary>
    public static class ClothesTypeDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<ClothesTypeEntity>> GetClothesTypeDbSet(IEnumerable<ClothesTypeEntity> clothesTypes) =>
            clothesTypes.AsQueryable().BuildMockDbSet();
    }
}