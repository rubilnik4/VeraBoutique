using System.ComponentModel.DataAnnotations;
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
        public string Name { get; }
    }
}