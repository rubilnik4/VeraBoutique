using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
                                          IEnumerable<ColorClothesTransfer> colors, IEnumerable<SizeGroupTransfer> sizes,
                                          decimal price, byte[]? image)
            : base(id, name, price, image)
        {
            Description = description;
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
        public IReadOnlyCollection<ColorClothesTransfer> Colors { get; } = new List<ColorClothesTransfer>();

        /// <summary>
        /// Размеры
        /// </summary>
        [Required]
        public IReadOnlyCollection<SizeGroupTransfer> SizeGroups { get; } = new List<SizeGroupTransfer>();
    }
}