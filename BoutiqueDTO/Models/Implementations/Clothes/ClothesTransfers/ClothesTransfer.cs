using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers
{
    /// <summary>
    /// Одежда. Доменная модель
    /// </summary>
    public class ClothesTransfer : ClothesShortTransfer, IClothesTransfer
    {
        public ClothesTransfer()
        { }

        public ClothesTransfer(IClothesShort clothesShort, string description,
                               GenderTransfer gender, ClothesTypeShortTransfer clothesTypeShort,
                               IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizes)
            :this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image,
                  description, gender, clothesTypeShort, colors, sizes)
        { }

        public ClothesTransfer(int id, string name, decimal price, byte[]? image, string description,
                               GenderTransfer gender, ClothesTypeShortTransfer clothesTypeShort, 
                               IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizes)
            : base(id, name, price, image)
        {
            Description = description;
            Gender = gender;
            ClothesTypeShort = clothesTypeShort;
            Colors = colors.ToList();
            SizeGroups = sizes.ToList();
        }

        /// <summary>
        /// Описание
        /// </summary>
        [Required]
        public string Description { get; } = null!;

        /// <summary>
        /// Пол одежды
        /// </summary>
        [Required]
        public GenderTransfer Gender { get; } = null!;

        /// <summary>
        /// Вид одежды
        /// </summary>
        [Required]
        public ClothesTypeShortTransfer ClothesTypeShort { get; } = null!;

        /// <summary>
        /// Вид одежды
        /// </summary>
        [Required]
        public IReadOnlyCollection<ColorClothesTransfer> Colors { get; } = new List<ColorClothesTransfer>();

        /// <summary>
        /// Размеры
        /// </summary>
        [Required]
        public IReadOnlyCollection<SizeGroupTransfer> SizeGroups { get; } = new List<SizeGroupTransfer>();
    }
}