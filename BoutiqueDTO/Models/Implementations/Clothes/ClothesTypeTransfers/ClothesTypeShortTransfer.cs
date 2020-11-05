using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Вид одежды. Основная информация. Трансферная модель
    /// </summary>
    public class ClothesTypeShortTransfer : ClothesTypeTransfers.ClothesTypeTransfer, IClothesTypeShortTransfer
    {
        public ClothesTypeShortTransfer()
        { }

        public ClothesTypeShortTransfer(IClothesType clothesType, CategoryTransfer category, GenderTransfer gender)
          : this(clothesType.Name, category, gender)
        {
            Gender = gender;
        }

        public ClothesTypeShortTransfer(string name, CategoryTransfer category, GenderTransfer gender)
            :base(name, category)
        {
            Gender = gender;
        }

        /// <summary>
        /// Тип пола. Трансферная модель
        /// </summary>
        public GenderTransfer Gender { get; } = null!;
    }
}