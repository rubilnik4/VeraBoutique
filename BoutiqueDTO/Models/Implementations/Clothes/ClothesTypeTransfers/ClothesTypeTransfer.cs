using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTypeTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers
{
    public class ClothesTypeTransfer : ClothesTypeBase<CategoryTransfer, GenderTransfer>, IClothesTypeTransfer
    {
        public ClothesTypeTransfer(IClothesTypeShortBase clothesType, CategoryTransfer category,
                                   IEnumerable<GenderTransfer> genders)
            : this(clothesType.Name, category, genders.ToList())
        { }

        [JsonConstructor]
        public ClothesTypeTransfer(string name, CategoryTransfer category, IReadOnlyCollection<GenderTransfer> genders) 
            : base(name, category, genders)
        { }
    }
}