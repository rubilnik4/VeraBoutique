using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDTO.Models.Interfaces.Clothes.CategoryTransfers;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers
{
    /// <summary>
    /// Категория одежды. Трансферная модель
    /// </summary>
    public class CategoryTransfer : CategoryBase, ICategoryTransfer
    {
        public CategoryTransfer(ICategoryBase category)
            :this(category.Name)
        { }

        [JsonConstructor]
        public CategoryTransfer(string name)
            :base(name)
        { }
    }
}