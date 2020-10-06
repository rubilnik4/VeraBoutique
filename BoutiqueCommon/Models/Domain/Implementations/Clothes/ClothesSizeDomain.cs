using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public class ClothesSizeDomain : Size, IClothesSizeDomain
    {
        public ClothesSizeDomain(SizeType clothesSizeType, int size, string sizeName)
            : base(clothesSizeType, size, sizeName)
        { }
    }
}