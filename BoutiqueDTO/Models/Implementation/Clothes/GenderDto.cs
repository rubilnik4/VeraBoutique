using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueDTO.Models.Implementation.Clothes
{
    /// <summary>
    /// Пол для одежды. Трансферная модель
    /// </summary>
    public class GenderDto
    {
        public GenderDto()
        { }

        public GenderDto(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        [Required]
        public GenderType GenderType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}