using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains
{
    /// <summary>
    /// Категория одежды с подтипами. Доменная модель
    /// </summary>
    public interface ICategoryClothesTypeDomain: ICategoryClothesTypeBase<IClothesTypeDomain>, ICategoryDomain
    { }
}