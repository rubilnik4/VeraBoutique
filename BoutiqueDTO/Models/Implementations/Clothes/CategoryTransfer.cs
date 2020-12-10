using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
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