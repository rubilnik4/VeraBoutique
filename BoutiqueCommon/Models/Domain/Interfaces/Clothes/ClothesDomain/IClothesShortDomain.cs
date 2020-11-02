using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public interface IClothesShortDomain : IClothesShort, IDomainModel<int>
    { }
}