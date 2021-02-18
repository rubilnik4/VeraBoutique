using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    public class ClothesTypeMainDomain: ClothesTypeMainBase<ICategoryDomain>, IClothesTypeMainDomain
    {
        public ClothesTypeMainDomain(IClothesTypeBase clothesType, ICategoryDomain category)
          : base(clothesType.Name, category)
        { }

        public ClothesTypeMainDomain(string name, ICategoryDomain category)
           : base(name, category)
        { }
    }
}