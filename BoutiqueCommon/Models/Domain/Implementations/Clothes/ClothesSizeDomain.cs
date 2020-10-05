using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public class ClothesSizeDomain : ClothesSize, IClothesSizeDomain
    {
        public ClothesSizeDomain(ClothesSizeType clothesSizeType, int size, string sizeName)
            : base(clothesSizeType, size, sizeName)
        { }
    }
}