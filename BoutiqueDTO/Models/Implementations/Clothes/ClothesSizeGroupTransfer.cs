using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа. Трансферная модель
    /// </summary>
    public class ClothesSizeGroupTransfer : IClothesSizeGroupTransfer
    {
        public ClothesSizeGroupTransfer()
        { }

        public ClothesSizeGroupTransfer(ISize clothesSizeBase,
                                        IReadOnlyCollection<ISize> clothesSizesAdditional)
        {
            ClothesSizeBase = clothesSizeBase;
            Sizes = clothesSizesAdditional;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Размеры одежды
        /// </summary>
        [Required]
        public ISize ClothesSizeBase { get; }

        /// <summary>
        /// Размеры одежды
        /// </summary>
        [Required]
        public IReadOnlyCollection<ISize> Sizes { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name => SizeGroup.GetGroupName(ClothesSizeBase, Sizes);
    }
}