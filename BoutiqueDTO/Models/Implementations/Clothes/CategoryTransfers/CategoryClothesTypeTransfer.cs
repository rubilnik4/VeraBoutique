using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды с подтипами. Трансферная модель
    /// </summary>
    public class CategoryClothesTypeTransfer: CategoryClothesTypeBase<ClothesTypeTransfer>, ICategoryClothesTypeTransfer
    {
        public CategoryClothesTypeTransfer(ICategoryBase category, IEnumerable<ClothesTypeTransfer> clothesTypes)
          : this(category.Name, clothesTypes)
        { }

        [JsonConstructor]
        public CategoryClothesTypeTransfer(string name, IEnumerable<ClothesTypeTransfer> clothesTypes)
            : base(name, clothesTypes)
        { }
    }
}