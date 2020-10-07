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
        public SizeTransfer(SizeType clothesSizeType, string sizeName)
        {
            SizeType = clothesSizeType;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (SizeType, string) Id => (SizeType, SizeName);

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        [Required]
        public SizeType SizeType { get; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        [Required]
        public string SizeName { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        public string SizeNameShort => SizeNaming.GetSizeNameShort(SizeType, SizeName);
    }
}