using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
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
    /// Тестовые данные таблицы типа пола
    /// </summary>
    public static class GenderTableMock
    {
        /// <summary>
        /// Таблица базы данных типа пола
        /// </summary>
        public static IGenderTable GetGenderTable(IEnumerable<GenderEntity> genders) =>
            new GenderTable(GenderDatabaseSetMock.GetGenderDbSet(genders).Object);
    }
}