using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueDTO.Models.Implementation.Clothes
{
    /// <summary>
    /// Пол для одежды. Трансферная модель
    /// </summary>
    public class GenderDto
    {
        public GenderDto(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderType GenderType { get;  }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get;  }
    }
}