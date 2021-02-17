using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Тип пола. Доменная модель
    /// </summary>
    public class GenderDomain: GenderBase, IGenderDomain
    {
        public GenderDomain(IGenderBase gender)
           : this(gender.GenderType, gender.Name)
        { }
        
        public GenderDomain(GenderType genderType, string name)
            : base(genderType, name)
        { }
    }
}