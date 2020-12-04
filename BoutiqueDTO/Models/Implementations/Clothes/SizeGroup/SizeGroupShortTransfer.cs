using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;

namespace BoutiqueDTO.Models.Implementations.Clothes.SizeGroup
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные. Трансферная модель
    /// </summary>
    public class SizeGroupShortTransfer : ISizeGroupShortTransfer
    {
        public SizeGroupShortTransfer()
        { }

        public SizeGroupShortTransfer(ISizeGroup sizeGroup)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }

        public SizeGroupShortTransfer(ClothesSizeType clothesSizeType, int sizeNormalize)
        {
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (ClothesSizeType, int) Id => (ClothesSizeType, SizeNormalize);

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        [Required]
        public ClothesSizeType ClothesSizeType { get; set; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        [Required]
        public int SizeNormalize { get; set; }
    }
}