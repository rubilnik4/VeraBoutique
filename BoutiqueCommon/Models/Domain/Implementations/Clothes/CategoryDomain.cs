using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Категория одежды. Доменная модель
    /// </summary>
    public class CategoryDomain : Category, ICategoryDomain
    {
        public CategoryDomain(string name)
            : base(name)
        { }
    }
}