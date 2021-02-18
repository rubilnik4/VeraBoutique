using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.GenderTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers
{
    /// <summary>
    /// Тип пола с категориями. Трансферная модель
    /// </summary>
    public class GenderCategoryTransfer: GenderCategoryBase<CategoryClothesTypeTransfer, ClothesTypeTransfer> ,IGenderCategoryTransfer
    {
        public GenderCategoryTransfer(IGenderBase gender, IEnumerable<CategoryClothesTypeTransfer> categories)
          : this(gender.GenderType, gender.Name, categories)
        { }

        [JsonConstructor]
        public GenderCategoryTransfer(GenderType genderType, string name, IEnumerable<CategoryClothesTypeTransfer> categories)
            : base(genderType, name, categories)
        { }
    }
}