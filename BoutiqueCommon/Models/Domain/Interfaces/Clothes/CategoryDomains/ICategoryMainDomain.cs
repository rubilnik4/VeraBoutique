using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains
{
    /// <summary>
    /// Категория одежды. Основная модель. Доменная модель
    /// </summary>
    public interface ICategoryMainDomain: ICategoryMainBase<IGenderDomain>, ICategoryDomain
    { }
}