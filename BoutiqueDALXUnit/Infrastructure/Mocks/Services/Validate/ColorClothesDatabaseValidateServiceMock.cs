using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы цвета одежды
    /// </summary>
    public static class ColorClothesDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы цвета одежды
        /// </summary>
        public static IColorClothesDatabaseValidateService GetColorClothesDatabaseValidateService(IEnumerable<ColorEntity> colors) =>
            new ColorClothesDatabaseValidateService(ColorClothesTableMock.GetColorClothesTable(colors));
    }
}