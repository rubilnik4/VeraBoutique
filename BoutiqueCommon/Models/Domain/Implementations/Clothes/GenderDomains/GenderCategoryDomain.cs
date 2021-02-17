using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains
{
    /// <summary>
    /// Тип пола с категориями одежды. Доменная модель
    /// </summary>
    public class GenderCategoryDomain: GenderCategoryBase<ICategoryClothesTypeDomain, IClothesTypeDomain>, IGenderCategoryDomain
    {
        public GenderCategoryDomain(IGenderBase gender, IEnumerable<ICategoryClothesTypeDomain> categories)
          : this(gender.GenderType, gender.Name, categories)
        { }

        public GenderCategoryDomain(GenderType genderType, string name, IEnumerable<ICategoryClothesTypeDomain> categories)
            : base(genderType, name, categories)
        { }
    }
}