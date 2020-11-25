using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;

namespace BoutiqueDTO.Models.Implementations.Clothes.SizeGroup
{
    /// <summary>
    /// Группа размеров одежды разного типа. Трансферная модель
    /// </summary>
    public class SizeGroupTransfer : SizeGroupShortTransfer, ISizeGroupTransfer
    {
        public SizeGroupTransfer()
        { }
        public SizeGroupTransfer(ISizeGroup sizeGroup, IEnumerable<SizeTransfer> sizes)
          : this (sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize, sizes)
        { }

        public SizeGroupTransfer(ClothesSizeType clothesSizeType, int sizeNormalize, IEnumerable<SizeTransfer> sizes)
            :base(clothesSizeType, sizeNormalize)
        {
            Sizes = sizes.ToList();
        }

        /// <summary>
        /// Дополнительные размеры одежды
        /// </summary>
        [Required]
        public IReadOnlyCollection<SizeTransfer> Sizes { get; } = null!;
    }
}