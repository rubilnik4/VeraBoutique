using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet
{
    /// <summary>
    /// Сущности базы данных изображений
    /// </summary>
    public class ClothesImageDatabaseSetMock
    {
        /// <summary>
        /// Сущности базы данных
        /// </summary>
        public static Mock<DbSet<ClothesImageEntity>> GetClothesImageDbSet(IEnumerable<ClothesImageEntity> clothesImages) =>
            clothesImages.AsQueryable().BuildMockDbSet();
    }
}