using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using Functional.FunctionalExtensions.Sync;
using MockQueryable.Moq;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Тестовые данные таблицы группы размеров
    /// </summary>
    public static class SizeGroupTableMock
    {
        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        public static Mock<ISizeGroupTable> GetSizeGroupTable(Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> sizeGroupFunc) =>
            new Mock<ISizeGroupTable>().
            Void(mock => mock.Setup(sizeGroupTable => sizeGroupTable.Where(It.IsAny<(ClothesSizeType, int)>())).
                              Returns(sizeGroupFunc));


        /// <summary>
        /// Функция поиска группы размеров
        /// </summary>
        public static Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> SizeGroupOk(IEnumerable<SizeGroupEntity> sizeGroupEntities) =>
            sizeGroupId => sizeGroupEntities.Where(sizeGroup => sizeGroup.Id == sizeGroupId).
                                             AsQueryable().BuildMock().Object;

        /// <summary>
        /// Функция поиска группы размеров. Элемент не найден
        /// </summary>
        public static Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> SizeGroupNotFound() =>
            _ => Enumerable.Empty<SizeGroupEntity>().
                            AsQueryable().BuildMock().Object;

        /// <summary>
        /// Функция поиска группы размеров. Ошибка
        /// </summary>
        public static Func<(ClothesSizeType, int), IQueryable<SizeGroupEntity>> SizeGroupException() =>
            _ => throw new Exception();
    }
}