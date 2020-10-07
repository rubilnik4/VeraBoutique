using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public class SizeDomain : Size, ISizeDomain
    {
        public SizeDomain(SizeType sizeType, int sizeValue, string sizeName)
            : base(sizeType, sizeValue, sizeName)
        { }
    }
}