using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды. Трансферная модель
    /// </summary>
    public class ClothesTypeTransfer : IClothesTypeTransfer
    {
        public ClothesTypeTransfer()
        {

        }

        public ClothesTypeTransfer(string name, IEnumerable<GenderType> genders)
        {
            Name = name;
            Genders = genders.ToList().AsReadOnly();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; }

        /// <summary>
        /// Типы пола
        /// </summary>
        [Required]
        public IReadOnlyCollection<GenderType> Genders { get; }
    }
}