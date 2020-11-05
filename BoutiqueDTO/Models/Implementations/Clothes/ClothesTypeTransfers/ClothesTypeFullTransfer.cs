using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    public class ClothesTypeFullTransfer : ClothesTypeTransfers.ClothesTypeTransfer, IClothesTypeFullTransfer
    {
        public ClothesTypeFullTransfer()
        { }

        public ClothesTypeFullTransfer(IClothesType clothesType, CategoryTransfer category, 
                                       IEnumerable<GenderTransfer> genders)
          : this(clothesType.Name, category, genders)
        { }

        public ClothesTypeFullTransfer(string name, CategoryTransfer category,
                                       IEnumerable<GenderTransfer> genders)
            :base(name, category)
        {
            Genders = genders.ToList();
        }

        /// <summary>
        /// Типы пола. Трансферная модель
        /// </summary>
        public IReadOnlyCollection<GenderTransfer> Genders { get; } = null!;
    }
}