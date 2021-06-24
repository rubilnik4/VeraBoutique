using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public class ClothesTypeDomain : ClothesTypeBase, IClothesTypeDomain
    {
        public ClothesTypeDomain(IClothesTypeBase clothesType)
            : this(clothesType.Name, clothesType.SizeTypeDefault, clothesType.CategoryName)
        { }

        public ClothesTypeDomain(string name, SizeType sizeTypeDefault, string categoryName)
            : base(name, sizeTypeDefault, categoryName)
        { }
    }
}