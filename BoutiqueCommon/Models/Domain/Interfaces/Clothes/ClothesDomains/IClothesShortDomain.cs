using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Базовая доменная модель
    /// </summary>
    public interface IClothesShortDomain : IClothesShort, IDomainModel<int>
    { }
}