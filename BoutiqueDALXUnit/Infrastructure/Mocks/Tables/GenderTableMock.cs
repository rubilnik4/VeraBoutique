using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Services.ClothesType.Mocks
{
    /// <summary>
    /// Тестовые данные таблицы типа пола
    /// </summary>
    public static class GenderTableMock
    {
        /// <summary>
        /// Таблица базы данных типа пола
        /// </summary>
        public static Mock<IGenderTable> GetGenderTable(Func<GenderType, IQueryable<GenderEntity>> genderFunc) =>
            new Mock<IGenderTable>().
            Void(mock => mock.Setup(genderTable => genderTable.Where(It.IsAny<GenderType>())).
                              Returns(genderFunc));

        /// <summary>
        /// Функция получения типа пола
        /// </summary>
        public static Func<GenderType, IQueryable<GenderEntity>> GetGenderOk(IEnumerable<GenderEntity> genderEntities) =>
            genderType => genderEntities.Where(genderEntity => genderEntity.Id == genderType).
                                         AsQueryable().BuildMock().Object;

        /// <summary>
        /// Функция получения типа пола с ошибкой
        /// </summary>
        public static Func<GenderType, IQueryable<GenderEntity>> GetGenderException() =>
            _ => throw new Exception();
    }
}