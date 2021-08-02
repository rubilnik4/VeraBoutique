using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Тестовые данные таблицы изображений
    /// </summary>
    public static class ClothesImageTableMock
    {
        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        public static IClothesImageTable GetClothesImageTable(IEnumerable<ClothesImageEntity> clothesImages) =>
            new ClothesImageTable(ClothesImageDatabaseSetMock.GetClothesImageDbSet(clothesImages).Object);
    }
}