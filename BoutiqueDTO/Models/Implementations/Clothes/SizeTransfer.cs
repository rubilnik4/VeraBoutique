using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Трансферная модель
    /// </summary>
    public class SizeTransfer : ISizeTransfer
    {
        public SizeTransfer()
        { }

        public SizeTransfer(ISize size)
            :this(size.SizeType, size.Name)
        { }

        public SizeTransfer(SizeType clothesSizeType, string sizeName)
        {
            SizeType = clothesSizeType;
            Name = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (SizeType, string) Id => (SizeType, Name);

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        [Required]
        public SizeType SizeType { get; set; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}