using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Основная модель. Доменная модель
    /// </summary>
    public interface IClothesTypeMainDomain:IClothesTypeMainBase<ICategoryDomain>, IClothesTypeDomain
    { }
}