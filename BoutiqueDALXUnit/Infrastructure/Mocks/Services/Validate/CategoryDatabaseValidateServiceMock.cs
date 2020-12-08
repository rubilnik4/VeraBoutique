using System.Collections.Generic;
using BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate;
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
            new CategoryDatabaseValidateService(CategoryTableMock.GetCategoryTable(categories));
    }
}