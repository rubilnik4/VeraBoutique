using BoutiqueCommon.Models.Common.Implementations.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.GenderTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers
{
    /// <summary>
    /// Тип пола. Трансферная модель
    /// </summary>
    public class GenderTransfer : GenderBase, IGenderTransfer
    {
        public GenderTransfer(IGenderBase gender)
            : this(gender.GenderType, gender.Name)
        { }

        [JsonConstructor]
        public GenderTransfer(GenderType genderType, string name)
            :base(genderType, name)
        { }
    }
}