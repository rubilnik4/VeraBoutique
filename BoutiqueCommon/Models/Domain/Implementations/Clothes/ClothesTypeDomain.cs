using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public class ClothesTypeDomain : ClothesTypeBase, IClothesTypeDomain
    {
        public ClothesTypeDomain(IClothesTypeBase clothesType)
            : this(clothesType.Name, clothesType.CategoryName)
        { }

        public ClothesTypeDomain(string name, string categoryName)
            : base(name, categoryName)
        { }


    }
}