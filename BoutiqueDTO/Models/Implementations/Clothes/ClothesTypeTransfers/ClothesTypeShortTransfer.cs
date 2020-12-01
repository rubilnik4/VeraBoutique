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

        public ClothesTypeShortTransfer(IClothesType clothesType, string categoryName)
          : this(clothesType.Name, categoryName)
        { }

        public ClothesTypeShortTransfer(string name, string categoryName)
        {
            Name = name;
            CategoryName = categoryName;
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
        /// Категория
        /// </summary>
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}