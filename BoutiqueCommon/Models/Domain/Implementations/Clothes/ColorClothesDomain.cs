using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Цвет одежды. Доменная модель
    /// </summary>
    public class ColorClothesDomain : ColorClothes, IColorClothesDomain
    {
        public ColorClothesDomain(string name)
            : base(name)
        { }
    }
}