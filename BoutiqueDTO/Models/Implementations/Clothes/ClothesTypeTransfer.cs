using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    public class ClothesTypeTransfer : ClothesTypeShortTransfer, IClothesTypeTransfer
    {
        public ClothesTypeTransfer()
        { }

        public ClothesTypeTransfer(IClothesType clothesType, 
                                   GenderTransfer genderTransfer, CategoryTransfer categoryTransfer)
          : this(clothesType.Name, genderTransfer, categoryTransfer)
        { }

        public ClothesTypeTransfer(string name, GenderTransfer genderTransfer, CategoryTransfer categoryTransfer)
            :base(name)
        {
            GenderTransfer = genderTransfer;
            CategoryTransfer = categoryTransfer;
        }

        /// <summary>
        /// Категория одежды. Трансферная модель
        /// </summary>
        [Required]
        public CategoryTransfer CategoryTransfer { get; } = null!;

        /// <summary>
        /// Тип пола. Трансферная модель
        /// </summary>
        [Required]
        public GenderTransfer GenderTransfer { get; } = null!;
    }
}