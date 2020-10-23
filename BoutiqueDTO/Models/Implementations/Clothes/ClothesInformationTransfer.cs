using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
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

        public ClothesInformationTransfer(IClothesShort clothesShort,
                                          string description, ClothesTypeTransfer clothesTypeTransfer,
                                          IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizes)
            :this(clothesShort.Id, clothesShort.Name, 
                  clothesShort.Price, clothesShort.Image,
                  description, clothesTypeTransfer, colors, sizes)
        { }

        public ClothesInformationTransfer(int id, string name, decimal price, byte[]? image, 
                                          string description,  ClothesTypeTransfer clothesTypeTransfer,
                                          IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizes)
            : base(id, name, price, image)
        {
            Description = description;
            ClothesTypeTransfer = clothesTypeTransfer;
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
        public ClothesTypeTransfer ClothesTypeTransfer { get; } = null!;

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