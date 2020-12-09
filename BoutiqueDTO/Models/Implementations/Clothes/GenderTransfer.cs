using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
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