using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains
{
    /// <summary>
    /// Категория одежды с подтипами. Доменная модель
    /// </summary>
    public class CategoryClothesTypeDomain: CategoryClothesTypeBase<IClothesTypeDomain>, ICategoryClothesTypeDomain
    {
        public CategoryClothesTypeDomain(ICategoryBase category, IEnumerable<IClothesTypeDomain> clothesTypes)
             : this(category.Name, clothesTypes)
        { }

        public CategoryClothesTypeDomain(string name, IEnumerable<IClothesTypeDomain> clothesTypes)
            : base(name, clothesTypes)
        { }
    }
}