using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Категория одежды. Трансферные модели
    /// </summary>
    public static class CategoryTransfersData
    {
        /// <summary>
        /// Категория одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CategoryTransfer> CategoryTransfers =>
            CategoryData.CategoryDomains.
            Select(category => new CategoryTransfer(category)).
            ToList();
    }
}