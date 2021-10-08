using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    public static class ClothesTypeDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        public static IClothesTypeDatabaseValidateService GetClothesTypeDatabaseValidateService(IEnumerable<ClothesTypeEntity> clothesTypes) =>
            GetClothesTypeDatabaseValidateService(clothesTypes, Enumerable.Empty<CategoryEntity>());

        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        public static IClothesTypeDatabaseValidateService GetClothesTypeDatabaseValidateService(IEnumerable<ClothesTypeEntity> clothesTypes,
                                                                                                IEnumerable<CategoryEntity> categories) =>
            new ClothesTypeDatabaseValidateService(ClothesTypeTableMock.GetClothesTypeTable(clothesTypes),
                                                   CategoryDatabaseValidateServiceMock.GetCategoryDatabaseValidateService(categories));
    }
}