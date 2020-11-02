using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfer;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfer;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfer
{
    /// <summary>
    /// Одежда. Информация. Доменная модель
    /// </summary>
    public class ClothesFullTransfer : ClothesShortTransfer, IClothesFullTransfer
    {
        public ClothesFullTransfer()
        { }

        public ClothesFullTransfer(IClothesShort clothesShort, string description, ClothesTypeShortTransfer clothesTypeShortTransfer,
                               IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizes)
            :this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image,
                  description, clothesTypeShortTransfer, colors, sizes)
        { }

        public ClothesFullTransfer(int id, string name, decimal price, byte[]? image, string description,
                               ClothesTypeShortTransfer clothesTypeShort, IEnumerable<ColorClothesTransfer> colors,
                               IEnumerable<SizeGroupTransfer> sizes)
            : base(id, name, price, image)
        {
            Description = description;
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