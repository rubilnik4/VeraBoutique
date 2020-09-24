﻿using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Тип пола. Доменная модель
    /// </summary>
    public class GenderDomain: Gender, IGenderDomain
    {
        public GenderDomain(GenderType genderType, string name)
            : base(genderType, name)
        { }
    }
}