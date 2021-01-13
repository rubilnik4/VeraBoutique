using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;

namespace BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные. Трансферная модель
    /// </summary>
    public class SizeGroupShortTransfer : SizeGroupShortBase, ISizeGroupShortTransfer
    {
        public SizeGroupShortTransfer(ISizeGroupShortBase sizeGroup)
            : this(sizeGroup.ClothesSizeType, sizeGroup.SizeNormalize)
        { }

        [JsonConstructor]
        public SizeGroupShortTransfer(ClothesSizeType clothesSizeType, int sizeNormalize)
            :base(clothesSizeType, sizeNormalize)
        { }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id => GetIdHashCode(ClothesSizeType, SizeNormalize);
    }
}