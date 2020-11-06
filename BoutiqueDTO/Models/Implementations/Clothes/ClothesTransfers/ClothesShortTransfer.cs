using System.ComponentModel.DataAnnotations;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Базовая трансферная модель
    /// </summary>
    public class ClothesShortTransfer: IClothesShortTransfer
    {
        public ClothesShortTransfer()
        { }

        public ClothesShortTransfer(int id, string name, decimal price, byte[]? image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; } = null!;

        /// <summary>
        /// Цена
        /// </summary>
        [Required]
        public decimal Price { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[]? Image { get; }
    }
}