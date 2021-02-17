using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
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
            GetClothesTypeDatabaseValidateService(clothesTypes, Enumerable.Empty<CategoryEntity>(), Enumerable.Empty<GenderEntity>());

        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        public static IClothesTypeDatabaseValidateService GetClothesTypeDatabaseValidateService(IEnumerable<ClothesTypeEntity> clothesTypes,
                                                                                                IEnumerable<CategoryEntity> categories,
                                                                                                IEnumerable<GenderEntity> genders) =>
            new ClothesTypeDatabaseValidateService(ClothesTypeTableMock.GetClothesTypeTable(clothesTypes),
                                                   CategoryDatabaseValidateServiceMock.GetCategoryDatabaseValidateService(categories),
                                                   GenderDatabaseValidateServiceMock.GetGenderDatabaseValidateService(genders));
    }
}