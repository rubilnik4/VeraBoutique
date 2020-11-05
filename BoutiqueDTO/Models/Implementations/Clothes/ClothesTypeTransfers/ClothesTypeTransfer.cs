using System.ComponentModel.DataAnnotations;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    public abstract class ClothesTypeTransfer : IClothesTypeTransfer
    {
        protected ClothesTypeTransfer()
        { }

        protected ClothesTypeTransfer(string name, CategoryTransfer category)
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