using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
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
    /// Тестовые данные таблицы группы размеров
    /// </summary>
    public static class SizeGroupTableMock
    {
        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        public static ISizeGroupTable GetSizeGroupTable(IEnumerable<SizeGroupEntity> sizeGroups) =>
            new SizeGroupTable(SizeGroupDatabaseSetMock.GetSizeGroupDbSet(sizeGroups).Object);
    }
}