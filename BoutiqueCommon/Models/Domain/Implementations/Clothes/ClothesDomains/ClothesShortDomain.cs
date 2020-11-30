using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesShortDomain : ClothesMain, IClothesShortDomain
    {
        public ClothesShortDomain(int id, string name, string description, decimal price, byte[]? image)
            : base(id, name, description, price, image)
        { }
    }
}