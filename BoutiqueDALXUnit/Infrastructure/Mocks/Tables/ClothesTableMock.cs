using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;
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

        /// <summary>
        /// Получить таблицу одежды
        /// </summary>
        public static Mock<IClothesTable> GetClothesTable(IResultCollection<ClothesEntity> clothesEntities) =>
            new Mock<IClothesTable>().
            Void(tableMock => tableMock.Setup(table => table.FindsExpressionAsync(It.IsAny<Func<IQueryable<ClothesEntity>, IQueryable<ClothesEntity>>>())).
                              ReturnsAsync(clothesEntities));

        /// <summary>
        /// Получить таблицу одежды
        /// </summary>
        public static Mock<IClothesTable> GetClothesTable(IResultValue<ClothesImageEntity> clothesImageEntity) =>
            new Mock<IClothesTable>().
            Void(tableMock => tableMock.Setup(table => table.FindExpressionAsync(It.IsAny<Func<IQueryable<ClothesEntity>, Task<ClothesImageEntity?>>>(),
                                                                                 It.IsAny<int>())).
                              ReturnsAsync(clothesImageEntity));
    }
}