using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    public class ClothesDomain : ClothesBase<IGenderDomain, IClothesTypeShortDomain, IColorDomain, ISizeGroupDomain, ISizeDomain>, 
                                 IClothesDomain
    {
        public ClothesDomain(IClothesShortBase clothesShort,
                             IGenderDomain gender, IClothesTypeShortDomain clothesTypeShort,
                             IEnumerable<IColorDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
           : this(clothesShort.Id, clothesShort.Name, clothesShort.Description, clothesShort.Price, clothesShort.Image,
                  gender, clothesTypeShort, colors, sizeGroups)
        { }

        public ClothesDomain(int id, string name, string description, decimal price, byte[] image,
                             IGenderDomain gender, IClothesTypeShortDomain clothesTypeShort,
                             IEnumerable<IColorDomain> colors, IEnumerable<ISizeGroupDomain> sizeGroups)
            : base(id, name, description, price, image, gender, clothesTypeShort, colors, sizeGroups)
        { }
    }
}