using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueDTO.Models.Implementation.Clothes
{
    /// <summary>
    /// Пол для одежды. Трансферная модель
    /// </summary>
   
    public class GenderDto
    {
        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderType GenderType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}