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
        {

        }

        public ClothesInformationTransfer(int id, string name, string description, GenderType genderType,
                                          string clothesType, IReadOnlyCollection<int> sizes,
                                          decimal price, byte[]? image)
            : base(id, name, price, image)
        {
            Description = description;
            GenderType = genderType;
            ClothesType = clothesType;
            Sizes = sizes;
        }

        /// <summary>
        /// Описание
        /// </summary>
        [Required]
        public string Description { get; } = null!;

        /// <summary>
        /// Пол
        /// </summary>
        [Required]
        public GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        [Required]
        public string ClothesType { get; } = null!;

        /// <summary>
        /// Размеры
        /// </summary>
        [Required]
        public IReadOnlyCollection<int> Sizes { get; } = new List<int>();
    }
}