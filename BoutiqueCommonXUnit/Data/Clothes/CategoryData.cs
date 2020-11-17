using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

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
        public static List<ICategoryDomain> CategoryDomain =>
            new List<ICategoryDomain>
            {
                new CategoryDomain("Верхняя одежда"),
                new CategoryDomain("Побрякушки"),
            };
    }
}