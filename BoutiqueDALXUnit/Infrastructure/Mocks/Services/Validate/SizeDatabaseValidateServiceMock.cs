using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы пола одежды
    /// </summary>
    public static class SizeDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        public static ISizeDatabaseValidateService GetSizeDatabaseValidateService(IEnumerable<SizeEntity> sizes) =>
            new SizeDatabaseValidateService(SizeTableMock.GetSizeTable(sizes));
    }
}