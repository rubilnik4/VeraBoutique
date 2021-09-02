using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using ResultFunctional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Тестовые данные таблицы типа одежды
    /// </summary>
    public static class ClothesTypeTableMock
    {
        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        public static IClothesTypeTable GetClothesTypeTable(IEnumerable<ClothesTypeEntity> clothesTypes) =>
            new ClothesTypeTable(ClothesTypeDatabaseSetMock.GetClothesTypeDbSet(clothesTypes).Object);
    }
}