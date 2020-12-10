using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables.DatabaseSet;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Tables
{
    /// <summary>
    /// Таблица базы данных цвета одежды
    /// </summary>
    public static class ColorClothesTableMock
    {
        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public static IColorClothesTable GetColorClothesTable(IEnumerable<ColorEntity> colors) =>
            new ColorClothesTable(ColorClothesDatabaseSetMock.GetColorClothesDbSet(colors).Object);
    }
}