using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Таблица базы данных размеров одежды
    /// </summary>
    public static class SizeTableMock
    {
        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        public static ISizeTable GetSizeTable(IEnumerable<SizeEntity> sizes) =>
            new SizeTable(SizeDatabaseSetMock.GetSizeDbSet(sizes).Object);
    }
}