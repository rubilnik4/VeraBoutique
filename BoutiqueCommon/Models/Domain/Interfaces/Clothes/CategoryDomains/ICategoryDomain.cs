using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains
{
    /// <summary>
    /// Категория одежды. Доменная модель
    /// </summary>
    public interface ICategoryDomain : ICategoryBase, IDomainModel<string>
    { }
}