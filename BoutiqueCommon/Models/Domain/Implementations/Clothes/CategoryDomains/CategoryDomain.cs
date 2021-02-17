using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains
{
    /// <summary>
    /// Категория одежды. Доменная модель
    /// </summary>
    public class CategoryDomain : CategoryBase, ICategoryDomain
    {
        public CategoryDomain(ICategoryBase category)
          : base(category.Name)
        { }

        public CategoryDomain(string name)
            : base(name)
        { }
    }
}