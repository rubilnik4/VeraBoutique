using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDALXUnit.Infrastructure.Mocks.Tables;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Services.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы категорий одежды
    /// </summary>
    public static class CategoryDatabaseValidateServiceMock
    {
        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        public static ICategoryDatabaseValidateService GetCategoryDatabaseValidateService(IEnumerable<CategoryEntity> categories) =>
            GetCategoryDatabaseValidateService(categories, Enumerable.Empty<GenderEntity>());

        /// <summary>
        /// Сервис проверки данных из базы категорий одежды
        /// </summary>
        public static ICategoryDatabaseValidateService GetCategoryDatabaseValidateService(IEnumerable<CategoryEntity> categories,
                                                                                          IEnumerable<GenderEntity> genders) =>
            new CategoryDatabaseValidateService(CategoryTableMock.GetCategoryTable(categories),
                                                GenderDatabaseValidateServiceMock.GetGenderDatabaseValidateService(genders));
    }
}