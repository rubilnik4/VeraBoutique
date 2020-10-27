using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks
{
    /// <summary>
    /// Тестовые данные таблицы одежды
    /// </summary>
    public static class ClothesTableMock
    {
        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        public static Mock<IClothesTable> GetClothesTable(Func<int, IQueryable<ClothesInformationEntity>> clothesInformationFunc) =>
            new Mock<IClothesTable>().
            Void(mock => mock.Setup(clothesTable => clothesTable.Where(It.IsAny<int>())).
                              Returns(clothesInformationFunc));

        /// <summary>
        /// Функция поиска информации об одежде
        /// </summary>
        public static Func<int, IQueryable<ClothesInformationEntity>> GetClothesInformationOk(IEnumerable<ClothesInformationEntity> clothesInformationEntities) =>
            id => clothesInformationEntities.Where(clothesInformation => clothesInformation.Id == id).
                                             AsQueryable().BuildMock().Object;

        /// <summary>
        /// Функция поиска информации об одежде. Элемент не найден
        /// </summary>
        public static Func<int, IQueryable<ClothesInformationEntity>> GetClothesInformationNotFound() =>
            _ => Enumerable.Empty<ClothesInformationEntity>().
                            AsQueryable().BuildMock().Object;

        /// <summary>
        /// Функция поиска информации об одежде. Ошибка
        /// </summary>
        public static Func<int, IQueryable<ClothesInformationEntity>> GetClothesInformationException() =>
            _ => throw new Exception();
    }
}