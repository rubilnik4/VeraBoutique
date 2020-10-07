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

        public SizeTransfer(SizeType clothesSizeType, int sizeValue, string sizeName)
        {
            SizeType = clothesSizeType;
            SizeValue = sizeValue;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (SizeType, int) Id => (SizeType, SizeValue);

        /// <summary>
        /// Тип размера одежды
        /// </summary>
        [Required]
        public SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        [Required]
        public int SizeValue { get; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        [Required]
        public string SizeName { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        public string ClothesSizeNameShort => SizeNaming.GetSizeNameShort(SizeType, SizeName);
    }
}