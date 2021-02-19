using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Тестовые данные таблицы одежды
    /// </summary>
    public static class ClothesTableMock
    {
        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public static IClothesTable GetClothesTable(IEnumerable<ClothesEntity> clothes) =>
            new ClothesTable(ClothesDatabaseSetMock.GetClothesDbSet(clothes).Object);
    }
}