using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды. Трансферная модель модель
    /// </summary>
    public class CategoryMainTransfer: CategoryMainBase<GenderTransfer>, ICategoryMainTransfer
    {
        public CategoryMainTransfer(ICategoryBase category, IEnumerable<GenderTransfer> genders)
            :this(category.Name, genders)
        { }

        public CategoryMainTransfer(string name, IEnumerable<GenderTransfer> genders)
            :base(name, genders)
        { }
    }
}