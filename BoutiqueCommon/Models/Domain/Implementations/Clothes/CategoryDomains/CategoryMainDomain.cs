using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains
{
    /// <summary>
    /// Категория одежды. Основная модель. Доменная модель
    /// </summary>
    public class CategoryMainDomain: CategoryMainBase<IGenderDomain>, ICategoryMainDomain
    {
        public CategoryMainDomain(ICategoryBase category, IEnumerable<IGenderDomain> genders)
            : this(category.Name, genders)
        { }

        public CategoryMainDomain(string name, IEnumerable<IGenderDomain> genders)
            :base(name, genders)
        { }
    }
}