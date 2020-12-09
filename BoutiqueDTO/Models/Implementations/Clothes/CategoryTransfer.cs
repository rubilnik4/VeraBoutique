using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Категория одежды. Трансферная модель
    /// </summary>
    public class CategoryTransfer : ICategoryTransfer
    {
        public CategoryTransfer()
        { }

        public CategoryTransfer(ICategoryBase category)
            :this(category.Name)
        { }

        public CategoryTransfer(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}