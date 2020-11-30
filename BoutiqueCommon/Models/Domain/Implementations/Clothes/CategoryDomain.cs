using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Категория одежды. Доменная модель
    /// </summary>
    public class CategoryDomain : Category, ICategoryDomain
    {
        public CategoryDomain(ICategory category)
          : base(category.Name)
        { }

        public CategoryDomain(string name)
            : base(name)
        { }
    }
}