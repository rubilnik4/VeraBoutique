using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Цвет одежды. Трансферная модель
    /// </summary>
    public class ColorClothesTransfer : IColorClothesTransfer
    {
        public ColorClothesTransfer()
        { }

        public ColorClothesTransfer(IColorBase color)
            :this (color.Name)
        { }

        public ColorClothesTransfer(string name)
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