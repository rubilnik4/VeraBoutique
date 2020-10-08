﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа. Трансферная модель
    /// </summary>
    public class SizeGroupTransfer : ISizeGroupTransfer
    {
        public SizeGroupTransfer()
        { }

        public SizeGroupTransfer(ClothesSizeType clothesSizeType, int sizeNormalize,
                                 IEnumerable<SizeTransfer> sizes)
        {
            ClothesSizeType = clothesSizeType;
            SizeNormalize = sizeNormalize;
            Sizes = sizes.ToList();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (ClothesSizeType, int) Id => (ClothesSizeType, SizeNormalize);

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        [Required]
        public ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        [Required]
        public int SizeNormalize { get; }

        /// <summary>
        /// Дополнительные размеры одежды
        /// </summary>
        [Required]
        public IReadOnlyCollection<SizeTransfer> Sizes { get; }
    }
}