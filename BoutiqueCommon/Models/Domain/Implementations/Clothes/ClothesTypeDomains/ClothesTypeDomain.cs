using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
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