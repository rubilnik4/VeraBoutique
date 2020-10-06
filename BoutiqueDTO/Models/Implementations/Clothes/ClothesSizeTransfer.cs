using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Трансферная модель
    /// </summary>
    public class ClothesSizeTransfer : IClothesSizeTransfer
    {
        public ClothesSizeTransfer()
        { }

        public ClothesSizeTransfer(SizeType clothesSizeType, int size, string sizeName)
        {
            SizeType = clothesSizeType;
            SizeValue = size;
            SizeName = sizeName;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => ClothesSizeNameShort;

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
        private string ClothesSizeNameShort => BoutiqueCommon.Models.Common.Implementations.Clothes.Size.GetClothesSizeNameShort(SizeType, SizeName);
    }
}