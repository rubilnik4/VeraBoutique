using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
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

        public ClothesShortTransfer(IClothesMain clothes, GenderType genderType, string clothesTypeName)
            :this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image, 
                  genderType, clothesTypeName)
        { }

        public ClothesShortTransfer(int id, string name, string description, decimal price, byte[]? image, 
                                    GenderType genderType, string clothesTypeName)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            GenderType = genderType;
            ClothesTypeName = clothesTypeName;
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
        /// Описание
        /// </summary>
        [Required]
        public string Description { get; } = null!;

        /// <summary>
        /// Цена
        /// </summary>
        [Required]
        public decimal Price { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        [Required]
        public byte[]? Image { get; }

        /// <summary>
        /// Тип пола одежды
        /// </summary>
        [Required]
        public GenderType GenderType { get; }

        /// <summary>
        /// Тип одежды
        /// </summary>
        [Required]
        public string ClothesTypeName { get; } = null!;
    }
}