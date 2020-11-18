using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using Functional.FunctionalExtensions.Sync;
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
        /// Таблица базы данных типа пола одежды
        /// </summary>
        public static Mock<IClothesTypeTable> GetClothesTypeTable(Func<string, IQueryable<BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities.ClothesTypeEntity>> clothesTypeFunc) =>
            new Mock<IClothesTypeTable>().
            Void(mock => mock.Setup(clothesTypeTable => clothesTypeTable.Where(It.IsAny<string>())).
                              Returns(clothesTypeFunc));

        /// <summary>
        /// Функция поиска информации об одежде
        /// </summary>
        public static Func<string, IQueryable<BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities.ClothesTypeEntity>> GetClothesTypeOk(IEnumerable<BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities.ClothesTypeEntity> clothesTypeEntities) =>
            clothesType => clothesTypeEntities.Where(clothesTypeEntity => clothesTypeEntity.Id == clothesType).
                                               AsQueryable().BuildMock().Object;
    }
}