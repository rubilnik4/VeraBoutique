using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains
{
    /// <summary>
    /// Вид одежды. Доменная модель
    /// </summary>
    public class ClothesTypeDomain : ClothesTypeBase<ICategoryDomain, IGenderDomain>, IClothesTypeDomain
    {
        public ClothesTypeDomain(IClothesTypeShortBase clothesTypeShort, ICategoryDomain category, 
                                 IEnumerable<IGenderDomain> genders)
            : this(clothesTypeShort.Name, category, genders)
        { }

        public ClothesTypeDomain(string name, ICategoryDomain category, IEnumerable<IGenderDomain> genders)
            : base(name, category, genders)
        { }
    }
}