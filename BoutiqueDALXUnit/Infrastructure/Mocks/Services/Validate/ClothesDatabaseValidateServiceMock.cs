using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы одежды
    /// </summary>
    public static class ClothesDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы одежды
        /// </summary>
        public static IClothesDatabaseValidateService GetClothesDatabaseValidateService(IEnumerable<ClothesEntity> clothes) =>
            new ClothesDatabaseValidateService(ClothesTableMock.GetClothesTable(clothes),
                                               new Mock<IGenderDatabaseValidateService>().Object,
                                               new Mock<IClothesTypeDatabaseValidateService>().Object,
                                               new Mock<IColorClothesDatabaseValidateService>().Object,
                                               new Mock<ISizeGroupDatabaseValidateService>().Object,
                                               new Mock<IClothesImageDatabaseValidateService>().Object);
    }
}