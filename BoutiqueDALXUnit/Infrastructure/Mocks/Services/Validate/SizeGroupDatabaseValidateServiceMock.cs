using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы группы размера одежды
    /// </summary>
    public static class SizeGroupDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы группы размера одежды
        /// </summary>
        public static ISizeGroupDatabaseValidateService GetSizeGroupDatabaseValidateService(IEnumerable<SizeGroupEntity> sizeGroups) =>
            GetSizeGroupDatabaseValidateService(sizeGroups, Enumerable.Empty<SizeEntity>());

        /// <summary>
        /// Сервис проверки данных из базы группы размера одежды
        /// </summary>
        public static ISizeGroupDatabaseValidateService GetSizeGroupDatabaseValidateService(IEnumerable<SizeGroupEntity> sizeGroups,
                                                                                            IEnumerable<SizeEntity> sizes) =>
            new SizeGroupDatabaseValidateService(SizeGroupTableMock.GetSizeGroupTable(sizeGroups),
                                                 SizeDatabaseValidateServiceMock.GetSizeDatabaseValidateService(sizes));
    }
}