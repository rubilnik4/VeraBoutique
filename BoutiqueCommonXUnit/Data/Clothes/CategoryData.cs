using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;

namespace BoutiqueCommonXUnit.Data.Clothes
{
    /// <summary>
    /// Данные категорий одежды
    /// </summary>
    public static class CategoryData
    {
        /// <summary>
        /// Получить категории одежды
        /// </summary>
        public static IReadOnlyCollection<ICategoryDomain> CategoryDomains =>
            new List<ICategoryDomain>
            {
                new CategoryDomain("Верхняя одежда"),
                new CategoryDomain("Побрякушки"),
            };
    }
}