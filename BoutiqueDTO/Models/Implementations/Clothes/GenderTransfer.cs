using System;
using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Тип пола. Трансферная модель
    /// </summary>
    public class GenderTransfer: IGenderTransfer
    {
        public GenderTransfer()
        { }

        public GenderTransfer(IGender gender)
            :this(gender.GenderType, gender.Name)
        { }

        public GenderTransfer(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public GenderType Id => GenderType;

        /// <summary>
        /// Тип пола
        /// </summary>
        [Required]
        public GenderType GenderType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}