using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;
using Moq;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    public static class ClothesImageDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы изображений одежды
        /// </summary>
        public static IClothesImageDatabaseValidateService GetClothesImageDatabaseValidateService(IEnumerable<ClothesImageEntity> clothesImage) =>
            new ClothesImageDatabaseValidateService(ClothesImageTableMock.GetClothesImageTable(clothesImage));
    }
}