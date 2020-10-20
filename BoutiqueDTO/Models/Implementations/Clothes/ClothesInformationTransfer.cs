using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Одежда. Информация. Доменная модель
    /// </summary>
    public class ClothesInformationTransfer : ClothesShortTransfer, IClothesInformationTransfer
    {
        public ClothesInformationTransfer()
        { }

        public ClothesInformationTransfer(int id, string name, string description,
                                          IReadOnlyCollection<string> colors, IReadOnlyCollection<int> sizes,
                                          decimal price, byte[]? image)
            : base(id, name, price, image)
        {
            Description = description;
            Colors = colors;
            Sizes = sizes;
        }

        /// <summary>
        /// Описание
        /// </summary>
        [Required]
        public string Description { get; } = null!;

        /// <summary>
        /// Вид одежды
        /// </summary>
        [Required]
        public IReadOnlyCollection<string> Colors { get; } = new List<string>();

        /// <summary>
        /// Размеры
        /// </summary>
        [Required]
        public IReadOnlyCollection<int> Sizes { get; } = new List<int>();
    }
}