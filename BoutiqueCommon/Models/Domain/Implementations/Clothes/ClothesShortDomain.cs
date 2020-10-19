using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesShortDomain : ClothesShort, IClothesShortDomain
    {
        public ClothesShortDomain(int id, string name,
                             decimal price, byte[]? image)
            : base(id, name, price, image)
        { }
    }
}