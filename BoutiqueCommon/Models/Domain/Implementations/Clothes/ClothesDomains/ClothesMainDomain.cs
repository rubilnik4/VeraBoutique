using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    public class ClothesMainDomain : ClothesMainBase<IGenderDomain, IClothesTypeDomain, IColorDomain, ISizeGroupMainDomain, ISizeDomain>, 
                                     IClothesMainDomain
    {
        public ClothesMainDomain(IClothesBase clothes, byte[] image,
                                 IGenderDomain gender, IClothesTypeDomain clothesType,
                                 IEnumerable<IColorDomain> colors, IEnumerable<ISizeGroupMainDomain> sizeGroups)
           : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, image, gender, clothesType, colors, sizeGroups)
        { }

        public ClothesMainDomain(int id, string name, string description, decimal price, byte[] image,
                                 IGenderDomain gender, IClothesTypeDomain clothesType,
                                 IEnumerable<IColorDomain> colors, IEnumerable<ISizeGroupMainDomain> sizeGroups)
            : base(id, name, description, price, image, gender, clothesType, colors, sizeGroups)
        { }
    }
}