using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Уточненная информация. Доменная модель
    /// </summary>
    public interface IClothesDetailDomain: IClothesDetailBase<IColorDomain, ISizeGroupMainDomain, ISizeDomain>, IClothesDomain
    { }
}