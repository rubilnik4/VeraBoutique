using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Базовая трансферная модель
    /// </summary>
    public class ClothesTypeShortTransfer : IClothesTypeShortTransfer
    {
        public ClothesTypeShortTransfer()
        { }

        public ClothesTypeShortTransfer(IClothesType clothesType, CategoryTransfer category)
          : this(clothesType.Name, category)
        { }

        public ClothesTypeShortTransfer(string name, CategoryTransfer category)
        {
            Name = name;
            Category = category;
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
        public CategoryTransfer Category { get; } = null!;
    }
}