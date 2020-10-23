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
        { }

        public ClothesTypeTransfer(string name, CategoryTransfer categoryTransfer)
        {
            Name = name;
            CategoryTransfer = categoryTransfer;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Категория одежды. Трансферная модель
        /// </summary>
        [Required]
        public CategoryTransfer CategoryTransfer { get; } = null!;
    }
}