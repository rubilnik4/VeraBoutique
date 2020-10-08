using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа. Трансферная модель
    /// </summary>
    public interface ISizeGroupTransfer : ISizeGroup, ITransferModel<(ClothesSizeType, int)>
    {
        /// <summary>
        /// Дополнительные размеры одежды
        /// </summary>
        IReadOnlyCollection<SizeTransfer> Sizes { get; }
    }
}