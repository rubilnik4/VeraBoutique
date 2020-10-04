using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные категорий одежды
    /// </summary>
    public static class CategoryData
    {
        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static List<ICategoryDomain> GetCategoryDomain() =>
            new List<ICategoryDomain>()
            {
                new CategoryDomain("Верхняя одежда"),
                new CategoryDomain("Побрякушки"),
            };
    }
}