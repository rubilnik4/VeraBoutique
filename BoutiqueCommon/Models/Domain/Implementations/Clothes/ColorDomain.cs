using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Цвет одежды. Доменная модель
    /// </summary>
    public class ColorDomain : ColorBase, IColorDomain
    {
        public ColorDomain(IColorBase color)
            :this(color.Name)
        { }
        
        public ColorDomain(string name)
            : base(name)
        { }
    }
}