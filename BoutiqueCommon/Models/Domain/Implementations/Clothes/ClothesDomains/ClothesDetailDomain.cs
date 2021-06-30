using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains
{
    /// <summary>
    /// Одежда. Уточненная информация. Доменная модель
    /// </summary>
    public class ClothesDetailDomain: ClothesDetailBase<IColorDomain, ISizeGroupMainDomain, ISizeDomain>,
                                      IClothesDetailDomain
    {
        public ClothesDetailDomain(IClothesBase clothes, 
                                   IEnumerable<IColorDomain> colors, IEnumerable<ISizeGroupMainDomain> sizeGroups)
           : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, 
                  clothes.GenderType, clothes.ClothesTypeName, colors, sizeGroups)
        { }

        public ClothesDetailDomain(int id, string name, string description, decimal price,
                                   GenderType genderType, string clothesTypeName,
                                   IEnumerable<IColorDomain> colors, IEnumerable<ISizeGroupMainDomain> sizeGroups)
            : base(id, name, description, price, genderType, clothesTypeName, colors, sizeGroups)
        { }
    }
}