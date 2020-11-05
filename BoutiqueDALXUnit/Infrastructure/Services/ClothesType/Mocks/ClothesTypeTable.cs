using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks
{
    /// <summary>
    /// Тестовые данные таблицы типа одежды
    /// </summary>
    public static class ClothesTypeTable
    {
        /// <summary>
        /// Таблица базы данных типа пола одежды
        /// </summary>
        public static Mock<IClothesTypeTable> GetClothesTypeTable(Func<string, IQueryable<ClothesTypeFullEntity>> clothesTypeFunc) =>
            new Mock<IClothesTypeTable>().
            Void(mock => mock.Setup(clothesTypeTable => clothesTypeTable.Where(It.IsAny<string>())).
                              Returns(clothesTypeFunc));

        /// <summary>
        /// Функция поиска информации об одежде
        /// </summary>
        public static Func<string, IQueryable<ClothesTypeFullEntity>> GetClothesTypeOk(IEnumerable<ClothesTypeFullEntity> clothesTypeEntities) =>
            clothesType => clothesTypeEntities.Where(clothesTypeEntity => clothesTypeEntity.Id == clothesType).
                                               AsQueryable().BuildMock().Object;
    }
}